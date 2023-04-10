using backend.Storage;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using backend.Battle;
using shared;
using Google.Protobuf;

namespace backend.Controllers;

public class WebSocketController : ControllerBase {
    private readonly ILogger _logger; // Dependency injection reference https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0#create-logs

    private readonly IAuthTokenCache _tokenCache;
    private readonly IRoomManager _roomManager;

    public WebSocketController(ILogger<WebSocketController> logger, IAuthTokenCache tokenCache, IRoomManager roomManager) {
        _logger = logger;
        _tokenCache = tokenCache;
        _roomManager = roomManager;
    }

    [HttpGet] // Adding this Attribute just for Swagger recognition :)
    [Route("/Ws")]
    public async Task Get([FromQuery] string authToken, [FromQuery] int playerId, [FromQuery] int speciesId) {
        /**
        [WARNING] The async/await pattern employed by C# is very different from Golang's goroutine, yet we still hold the same assumption: different sessions of different players might use different OS threads! 

        Another thing to keep in mind is that "await" DOESN'T put its task into another thread (reference https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model#BKMK_Threads), unless specified by "Task.Run(...)" (reference https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=netstandard-2.1). Therefore if we really want to mimic the behaviour with "go Xxx()", the closest way could be "async Task doHeavyWork(...); doHeavyWork(...).Start()".
        */
        if (HttpContext.WebSockets.IsWebSocketRequest) {
            _logger.LogInformation("Got a websocket connection request#1 [ authToken={0}, playerId={1} ]", authToken, playerId);
            if (_tokenCache.ValidateToken(authToken, playerId)) {
                _logger.LogInformation("Got a websocket connection request#2 Validated successfully [ authToken={0}, playerId={1}, speciesId={2} ]", authToken, playerId, speciesId);
                using var session = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await HandleNewPlayerPrimarySession(session, playerId, speciesId);
            } else {
                _logger.LogWarning("Got a websocket connection request#2 Failed validation [ authToken={0}, playerId={1} ]", authToken, playerId);
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        } else {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task HandleNewPlayerPrimarySession(WebSocket session, int playerId, int speciesId) {

        using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource()) {
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            WebSocketCloseStatus closeCode = WebSocketCloseStatus.Empty;
            string? closeReason = null;

            int addPlayerToRoomResult = ErrCode.UnknownError;
            Player player = new Player(new PlayerDownsync());

            try {
                var room = _roomManager.Pop();
                if (null == room) {
                    _logger.LogWarning("No available room [ playerId={0} ]", playerId);
                    return;
                }
                addPlayerToRoomResult = room.AddPlayerIfPossible(player, playerId, speciesId, session, cancellationTokenSource);
                if (ErrCode.Ok != addPlayerToRoomResult) {
                    _logger.LogWarning("Failed to add player to room [ roomId={0}, playerId={1}, result={2} ]", room.id, playerId, addPlayerToRoomResult);
                    return;
                }

                _logger.LogInformation("Added player to room [ roomId={0}, playerId={1} ]", room.id, playerId);
                _roomManager.Push(room.calRoomScore(), room);

                var bciFrame = new BattleColliderInfo {
                    StageName = room.stageName,

                    BoundRoomId = room.id,
                    BattleDurationFrames = room.battleDurationFrames,
                    InputFrameUpsyncDelayTolerance = room.inputFrameUpsyncDelayTolerance,
                    MaxChasingRenderFramesPerUpdate = room.maxChasingRenderFramesPerUpdate,

                    RenderBufferSize = room.GetRenderCacheSize(),
                    BoundRoomCapacity = room.capacity,
                    BattleUdpTunnel = room.battleUdpTunnelAddr,
                    FrameDataLoggingEnabled = room.frameDataLoggingEnabled
                };

                var initWsResp = new WsResp {
                    Ret = ErrCode.Ok,
                    Act = shared.Battle.DOWNSYNC_MSG_ACT_BATTLE_COLLIDER_INFO,
                    BciFrame = bciFrame,
                    PeerJoinIndex = player.PlayerDownsync.JoinIndex
                };

                var byteArr = initWsResp.ToByteArray();
                _logger.LogInformation("Sending bciFrame for [ roomId={0}, playerId={1}, messageLength={2} ]", room.id, playerId, byteArr.Length);
                await session.SendAsync(new ArraySegment<byte>(initWsResp.ToByteArray()), WebSocketMessageType.Binary, true, cancellationToken);

                var buffer = new byte[1024];
                while (!cancellationToken.IsCancellationRequested) {
                    try {
                        var receiveResult = await session.ReceiveAsync(
                        new ArraySegment<byte>(buffer), cancellationToken);

                        if (receiveResult.CloseStatus.HasValue) {
                            closeCode = receiveResult.CloseStatus.Value;
                            closeReason = receiveResult.CloseStatusDescription;
                            break;
                        }

                        WsReq pReq = WsReq.Parser.ParseFrom(buffer, 0, receiveResult.Count);
                        switch (pReq.Act) {
                            case shared.Battle.UPSYNC_MSG_ACT_PLAYER_COLLIDER_ACK:
                                var res1 = await room.OnPlayerBattleColliderAcked(playerId);
                                if (!res1) {
                                    if (!cancellationToken.IsCancellationRequested) {
                                        cancellationTokenSource.Cancel();
                                    }
                                } else {
                                    // [OPTIONAL]TODO: Popup this specific room from RoomManager, then re-push it with the updated score
                                }
                                break;
                            case shared.Battle.UPSYNC_MSG_ACT_PLAYER_CMD:
                                await room.OnBattleCmdReceived(pReq, false);
                                break;
                            default:
                                break;
                        }
                    } catch (OperationCanceledException ocEx) {
                        _logger.LogWarning("Session is cancelled for [ roomId={0}, playerId={1}, ocEx={2} ]", room.id, playerId, ocEx.Message);
                    } catch (Exception ex) {
                        _logger.LogWarning("Session got an exception for [ roomId={0}, playerId={1}, ocEx={2} ]", room.id, playerId, ex.Message);
                    }
                }

                if (ErrCode.Ok == addPlayerToRoomResult) {
                    room.OnPlayerDisconnected(playerId);
                }
            } finally {
                if (!cancellationToken.IsCancellationRequested) {
                    await session.CloseAsync(
                    closeCode,
                    closeReason,
                    CancellationToken.None); // We won't cancel the closing of a websocket session
                }
            }
        }
    }
}
