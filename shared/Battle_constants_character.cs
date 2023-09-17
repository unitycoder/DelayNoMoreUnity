using System.Collections.Generic;
using System.Collections.Immutable;
using static shared.CharacterState;

namespace shared {
    public partial class Battle {
        public static ImmutableDictionary<int, CharacterConfig> characters = ImmutableDictionary.Create<int, CharacterConfig>().AddRange(
                new[]
                {
                    new KeyValuePair<int, CharacterConfig>(0, new CharacterConfig {
                        SpeciesId = 0,
                        SpeciesName = "KnifeGirl",
                        Hp = 200,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(2.1f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        DashingEnabled = true,
                        OnWallEnabled = true,
                        WallJumpingFramesToRecover = 8,
                        WallJumpingInitVelX = (int)(3.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallJumpingInitVelY =  (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallSlidingVelY = (int)(-1 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetX = (int)(8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(48.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(100.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = true,
                        Hardness = 5
                    }),

                    new KeyValuePair<int, CharacterConfig>(1, new CharacterConfig {
                        SpeciesId = 1,
                        SpeciesName = "SwordMan",
                        Hp = 100,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(7 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        VisionOffsetX = (int)(8.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(16.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(20.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(20.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(40.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = false,
                        Hardness = 5
                    }),

                    new KeyValuePair<int, CharacterConfig>(2, new CharacterConfig {
                        SpeciesId = 2,
                        SpeciesName = "MonkGirl",
                        Hp = 230,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(7.5 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        DashingEnabled = true,
                        OnWallEnabled = true,
                        WallJumpingFramesToRecover = 8,
                        WallJumpingInitVelX = (int)(3.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallJumpingInitVelY =  (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallSlidingVelY = (int)(-1 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetX = (int)(19.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(130.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(28f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(46f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(46.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(28f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(46f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(90.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = true,
                        Hardness = 5
                    }),

                    new KeyValuePair<int, CharacterConfig>(3, new CharacterConfig {
                        SpeciesId = 3,
                        SpeciesName = "FireSwordMan",
                        Hp = 150,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(7.5 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        VisionOffsetX = (int)(8.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(16.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(100.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(95.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = false,
                        Hardness = 5
                    }),

                    new KeyValuePair<int, CharacterConfig>(4, new CharacterConfig {
                        SpeciesId = 4,
                        SpeciesName = "GunGirl",
                        Hp = 120,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 30,
                        Speed = (int)(1.6f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8.2 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        VisionOffsetX = (int)(8.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(16.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(100.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(36.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(95.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = true,
                        SlidingEnabled = true,
                        CrouchingEnabled = true,
                        Hardness = 5
                    }),

                    new KeyValuePair<int, CharacterConfig>(4096, new CharacterConfig {
                        SpeciesId = 4096,
                        SpeciesName = "BullWarrior",
                        Hp = 500,
                        OmitSoftPushback = true,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(10 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        VisionOffsetX = (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(16 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(300 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(160 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(140 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(140 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(140 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 2,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        CloseEnoughVirtualGridDistance = (int)(200.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        HasTurnAroundAnim = true,
                        Hardness = 8
                    }),
            });
    }
}
