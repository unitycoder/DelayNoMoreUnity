using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using shared;
using Google.Protobuf;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

public class WsSessionManager {
    // Reference https://github.com/paulbatum/WebSocket-Samples/blob/master/HttpListenerWebSocketEcho/Client/Client.cs

    /**
    I'm aware of that "C# ConcurrentQueue" is lock-free, thus safe to be accessed from the MainThread during "Update()" without introducing significant graphic lags. Reference https://devblogs.microsoft.com/pfxteam/faq-are-all-of-the-new-concurrent-collections-lock-free/.

    However a normal "Queue" is used here while it's still considered thread-safe in this particular case (even for multi-core cache, for why multi-core cache could be a source of data contamination in multithread context, see https://app.yinxiang.com/fx/6f48c146-7db8-4a64-bdf0-3c874cd9290d). A "Queue" is in general NOT thread-safe, but when "bool TryDequeue(out T)" is always called in "thread#A" while "void Enqueue(T)" being always called in "thread#B", we're safe, e.g. "thread#A == MainThread && thread#B == WebSocketThread" or viceversa. 
        
    I'm not using "RecvRingBuff" https://github.com/genxium/DelayNoMore/blob/v1.0.14-cc/frontend/build-templates/jsb-link/frameworks/runtime-src/Classes/ring_buff.cpp because WebSocket is supposed to be preserving send/recv order at all time.

    ################################################################################################################################################
    UPDATE 2024-04-16
    ################################################################################################################################################

    The normal "Queue" is planned to be changed into an "AsyncQueue" such that I can call "DequeueAsync" without spurious breaking when empty. All methods of "DequeueAsync" are thread-safe, but I'm not sure whether or not they're lock-free as well. Moreover, I haven't found a way to use https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.threading.asyncqueue-1?view=visualstudiosdk-2022 yet.

    Fairly speaking, the current

    ```
    while (wsSession is still open && not cancelled) { 
        while (senderBuffer.TryDequeue(out toSendObj)) {
            ...
            // process "toSendObj"
            ...
        }
        // having spurious breaking when empty here would just continue to another loop
    }
    ```

    implementation is immune to spurious breaking when empty too, but quite inefficient in CPU usage.

    ################################################################################################################################################
    UPDATE 2024-04-17
    ################################################################################################################################################

    The normal "Queue" of "senderBuffer" is changed into an "BlockingCollection" whose default underlying data structure is "ConcurrentQueue". The "recvBuffer" is left untouched because
    - its "TryDequeue" is only called by "OnlineMapController.pollAndHandleWsRecvBuffer" which wouldn't cause spurious breaking when empty (i.e. upon transient empty "recvBuffer", it's OK to poll in the next render frame), and
    - its "Enqueue" is driven by "wsSession.ReceiveAsync" which is cancellable.

    To my understanding, "BlockingCollection.TryTake(out dst, timeout, cancellationToken)" is equivalent to "dst = AsyncQueue.DequeueAsync(cancellationToken).Result", at least for my use case.
    */
    public BlockingCollection<WsReq> senderBuffer;
    private int sendBufferReadTimeoutMillis = 512;
    public Queue<WsResp> recvBuffer;
    private string uname;
    private string authToken; // cached for auto-login (TODO: link "save/load" of this value with persistent storage)
    private int playerId = Battle.TERMINATING_PLAYER_ID;
    private int speciesId = Battle.SPECIES_NONE_CH;

    public int GetPlayerId() {
        return playerId;
    }

    private static WsSessionManager _instance;

    public static WsSessionManager Instance {
        get {
            if (null == _instance) _instance = new WsSessionManager();
            return _instance;
        }
    }

    private WsSessionManager() {
        ClearCredentials();
        senderBuffer = new BlockingCollection<WsReq>();
        recvBuffer = new Queue<WsResp>();
    }

    public void SetCredentials(string theUname, string theAuthToken, int thePlayerId) {
        uname = theUname; 
        authToken = theAuthToken;
        playerId = thePlayerId;
    }

    public void SetSpeciesId(int theSpeciesId) {
        speciesId = theSpeciesId;
    }

    public int GetSpeciesId() {
        return speciesId;
    }

    public string GetUname() {
        return uname;
    }
    
    public void ClearCredentials() {
        SetCredentials(null, null, Battle.TERMINATING_PLAYER_ID);
        speciesId = Battle.SPECIES_NONE_CH;
    }

    public bool IsPossiblyLoggedIn() {
        return (Battle.TERMINATING_PLAYER_ID != playerId);
    }

    public async Task ConnectWsAsync(string wsEndpoint, CancellationToken cancellationToken, CancellationTokenSource cancellationTokenSource, CancellationTokenSource guiCanProceedSignalSource) {
        if (null == authToken || Battle.TERMINATING_PLAYER_ID == playerId) {
            string errMsg = String.Format("ConnectWs not having enough credentials, authToken={0}, playerId={1}, please go back to LoginPage!", authToken, playerId);
            throw new Exception(errMsg);
        }
        while (senderBuffer.TryTake(out _, sendBufferReadTimeoutMillis, cancellationToken)) { }
        recvBuffer.Clear();
        string fullUrl = wsEndpoint + String.Format("?authToken={0}&playerId={1}&speciesId={2}", authToken, playerId, speciesId);
        using (ClientWebSocket ws = new ClientWebSocket()) {
            try {
                await ws.ConnectAsync(new Uri(fullUrl), cancellationToken);
                var openMsg = new WsResp {
                    Ret = ErrCode.Ok,
                    Act = Battle.DOWNSYNC_MSG_WS_OPEN
                };
                recvBuffer.Enqueue(openMsg);
                guiCanProceedSignalSource.Cancel();
                await Task.WhenAll(Receive(ws, cancellationToken, cancellationTokenSource), Send(ws, cancellationToken));
                Debug.Log(String.Format("Both WebSocket 'Receive' and 'Send' tasks are ended."));
            } catch (OperationCanceledException ocEx) {
                Debug.LogWarning(String.Format("WsSession is cancelled for 'ConnectAsync'; ocEx.Message={0}", ocEx.Message));
            } catch (Exception ex) {
                Debug.LogWarning(String.Format("WsSession is stopped by exception; ex={0}", ex));
                // [WARNING] Edge case here, if by far we haven't signaled "guiCanProceedSignalSource", it means that the "characterSelectPanel" is still awaiting this signal to either proceed to battle or prompt an error message.  
                if (!guiCanProceedSignalSource.IsCancellationRequested) {
                    string errMsg = String.Format("ConnectWs failed before battle starts, authToken={0}, playerId={1}, please go back to LoginPage!", authToken, playerId);
                    throw new Exception(errMsg);
                } else {
                    var exMsg = new WsResp { 
                        Ret = ErrCode.UnknownError,
                        ErrMsg = ex.Message
                    };
                    recvBuffer.Enqueue(exMsg);
                }
            } finally {
                var closeMsg = new WsResp {
                    Ret = ErrCode.Ok,
                    Act = Battle.DOWNSYNC_MSG_WS_CLOSED
                };
                recvBuffer.Enqueue(closeMsg);

                if (!cancellationToken.IsCancellationRequested) {
                    try {
                        cancellationTokenSource.Cancel();
                    } catch (Exception ex) {
                        Debug.LogErrorFormat("Error cancelling ws session token source as a safe wrapping while it was checked not cancelled by far: {0}", ex);
                    }
                }
                Debug.LogWarning(String.Format("Enqueued DOWNSYNC_MSG_WS_CLOSED for main thread."));
            }
        }
    }

    private async Task Send(ClientWebSocket ws, CancellationToken cancellationToken) {
        Debug.Log(String.Format("Starts 'Send' loop, ws.State={0}", ws.State));
        WsReq toSendObj;
        try {
            while (WebSocketState.Open == ws.State && !cancellationToken.IsCancellationRequested) {
                if (senderBuffer.TryTake(out toSendObj, sendBufferReadTimeoutMillis, cancellationToken)) {
                    //Debug.Log("Ws session send: before");
                    var content = new ArraySegment<byte>(toSendObj.ToByteArray());
                    if (Battle.BACKEND_WS_RECV_BYTELENGTH < content.Count) {
                        Debug.LogWarning(String.Format("[content too big!] contentByteLength={0} > BACKEND_WS_RECV_BYTELENGTH={1}", content, Battle.BACKEND_WS_RECV_BYTELENGTH));
                    }
                    await ws.SendAsync(content, WebSocketMessageType.Binary, true, cancellationToken);
                    //Debug.Log(String.Format("'Send' loop, sent {0} bytes", toSendObj.ToByteArray().Length));
                }
            }
        } catch (OperationCanceledException ocEx) {
            Debug.LogWarning(String.Format("WsSession is cancelled for 'Send'; ocEx.Message={0}", ocEx.Message));
        } catch (Exception ex) {
            Debug.LogWarning(String.Format("WsSession is stopping for 'Send' upon exception; ex={0}", ex));
        } finally {
            Debug.Log(String.Format("Ends 'Send' loop, ws.State={0}", ws.State));
        }
    }

    private async Task Receive(ClientWebSocket ws, CancellationToken cancellationToken, CancellationTokenSource cancellationTokenSource) {
        Debug.Log(String.Format("Starts 'Receive' loop, ws.State={0}, cancellationToken.IsCancellationRequested={1}", ws.State, cancellationToken.IsCancellationRequested));
        byte[] byteBuff = new byte[Battle.FRONTEND_WS_RECV_BYTELENGTH];
        var arrSegBytes = new ArraySegment<byte>(byteBuff);
        try {
            while (WebSocketState.Open == ws.State) {
                //Debug.Log("Ws session recv: before");
                // FIXME: Without a "read timeout" parameter, it's unable to detect slow or halted ws session here!
                var result = await ws.ReceiveAsync(arrSegBytes, cancellationToken);
                //Debug.Log("Ws session recv: after");
                if (WebSocketMessageType.Close == result.MessageType) {
                    Debug.Log(String.Format("WsSession is asked by remote to close in 'Receive'"));
                    if (!cancellationToken.IsCancellationRequested) {
                        cancellationTokenSource.Cancel(); // To cancel the "Send" loop
                    }
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    Debug.LogWarning(String.Format("WsSession is closed in 'Receive'#1, ws.State={0}", ws.State));
                    return;
                } else {
                    try {
                        WsResp resp = WsResp.Parser.ParseFrom(byteBuff, 0, result.Count);
                        if (ErrCode.Ok != resp.Ret) {
                            Debug.LogWarning(String.Format("@playerRdfId={0}, unexpected Ret={1}, b64 content={2}", playerId, resp.Ret, Convert.ToBase64String(byteBuff, 0, result.Count)));
                        }
                        recvBuffer.Enqueue(resp);
                    } catch (Exception pbEx) {
                        Debug.LogWarning(String.Format("Protobuf parser exception is caught for 'Receive'; ex.Message={0}", pbEx.Message));
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        } catch (OperationCanceledException ocEx) {
            Debug.LogWarning(String.Format("WsSession is cancelled for 'Receive'; ocEx.Message={0}", ocEx.Message));
        } catch (Exception ex) {
            Debug.LogError(String.Format("WsSession is stopping for 'Receive' upon exception; ex={0}", ex));
        } finally {
            Debug.LogWarning(String.Format("Ends 'Receive' loop, ws.State={0}", ws.State));
        }
    }

    public delegate void OnLoginResult(int retCode, string? uname, int? playerId, string? authToken);

    public IEnumerator doCachedAutoTokenLoginAction(string httpHost, OnLoginResult? onLoginCallback) {
        string uri = httpHost + String.Format("/Auth/Token/Login");
        WWWForm form = new WWWForm();
        form.AddField("token", authToken); 
        form.AddField("playerId", playerId); 
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    if (null != onLoginCallback) {
                        onLoginCallback(ErrCode.UnknownError, null, null, null);
                    }
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    onLoginCallback(ErrCode.UnknownError, null, null, null);
                    break;
                case UnityWebRequest.Result.Success:
                    var res = JsonConvert.DeserializeObject<JObject>(webRequest.downloadHandler.text);
                    Debug.Log(String.Format("Received: {0}", res));
                    int retCode = res["retCode"].Value<int>();
                    if (ErrCode.Ok == retCode) {
                        var uname = res["uname"].Value<string>();
                        Debug.Log(String.Format("Token/Login succeeded, uname: {1}", uname));
                        if (null != onLoginCallback) {
                            onLoginCallback(ErrCode.Ok, uname, playerId, authToken);
                        }
                    } else {
                        ClearCredentials();
                        if (null != onLoginCallback) {
                            onLoginCallback(retCode, null, null, null);
                        }
                    }
                    break;
            }
        }
    }
}
