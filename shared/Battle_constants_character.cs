using System.Collections.Generic;
using System.Collections.Immutable;

namespace shared {
    public partial class Battle {
        public const int SPECIES_NONE_CH = -1;
        public const int SPECIES_BLADEGIRL = 0;
        public const int SPECIES_SWORDMAN = 1;
        public const int SPECIES_WITCHGIRL = 2;
        public const int SPECIES_FIRESWORDMAN = 3;
        public const int SPECIES_MAGSWORDGIRL = 6;

        public const int SPECIES_BULLWARRIOR = 4096;
        public const int SPECIES_GOBLIN = 4097;
        public const int SPECIES_SKELEARCHER = 4098;

        public const float DEFAULT_MIN_FALLING_VEL_Y_COLLISION_SPACE = -4.5f; 
        public const int DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID = (int)(DEFAULT_MIN_FALLING_VEL_Y_COLLISION_SPACE*COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO); 

        /**
         [WARNING] 

         The "SizeY" component of "BlownUp/LayDown/Dying" MUST be smaller or equal to that of "Shrinked", such that when a character is blown up an falled onto a "slip-jump provider", it wouldn't trigger an unexpected slip-jump.
         */
        public static ImmutableDictionary<int, CharacterConfig> characters = ImmutableDictionary.Create<int, CharacterConfig>().AddRange(
                new[]
                {
                    new KeyValuePair<int, CharacterConfig>(SPECIES_BLADEGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_BLADEGIRL,
                        SpeciesName = "BladeGirl",
                        Hp = 200,
                        Mp = 0,  
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 12,
                        LayDownFramesToRecover = 12,
                        GetUpInvinsibleFrames = 17,
                        GetUpFramesToRecover = 14,
                        Speed = (int)(2.1f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        DashingEnabled = true,
                        OnWallEnabled = true,
                        CrouchingEnabled = true,
                        CrouchingAtkEnabled = true, // It's actually weird to have "false == CrouchingAtkEnabled && true == CrouchingEnabled", but flexibility provided anyway
                        WallJumpingFramesToRecover = 8,
                        WallJumpingInitVelX = (int)(3.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallJumpingInitVelY =  (int)(8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        WallSlidingVelY = (int)(-1 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetX = (int)(8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO), // [WARNING] Being too "wide" can make "CrouchIdle1" bouncing on slopes!
                        DefaultSizeY = (int)(32.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        DefaultAirDashQuota = 1,
                        DefaultAirJumpQuota = 1,
                        UseIsolatedAvatar = true,
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedMagazineIv,
                                Quota = 1,
                                FramesToRecover = 0,
                                DefaultQuota = 1,
                                DefaultFramesToRecover = 15*BATTLE_DYNAMICS_FPS,
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                                SkillId = 4, // TODO: Remove this hardcoded "skillId"!
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_SWORDMAN, new CharacterConfig {
                        SpeciesId = SPECIES_SWORDMAN,
                        SpeciesName = "SwordMan",
                        Hp = 100,
                        Mp = 60*BATTLE_DYNAMICS_FPS, 
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 17,
                        LayDownFramesToRecover = 17,
                        GetUpInvinsibleFrames = 16,
                        GetUpFramesToRecover = 14,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(7.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
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
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_WITCHGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_WITCHGIRL,
                        SpeciesName = "WitchGirl",
                        Hp = 230,
                        Mp = 120*BATTLE_DYNAMICS_FPS, 
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 23,
                        LayDownFramesToRecover = 23,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8.8 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        DashingEnabled = true,
                        SlidingEnabled = true,
                        OnWallEnabled = false,
                        VisionOffsetX = (int)(8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO), // [WARNING] Being too "wide" can make "CrouchIdle1" bouncing on slopes!
                        DefaultSizeY = (int)(32.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 3,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 4, // Thus when hit by MagicPistolBullet she needs FramesToRecover!
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        DefaultAirDashQuota = 1,
                        DefaultAirJumpQuota = 0,
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedMagazineIv,
                                Quota = 1,
                                FramesToRecover = 0,
                                DefaultQuota = 1,
                                DefaultFramesToRecover = 15*BATTLE_DYNAMICS_FPS,
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                                SkillId = 27, // TODO: Remove this hardcoded "skillId"!
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_FIRESWORDMAN, new CharacterConfig {
                        SpeciesId = SPECIES_FIRESWORDMAN,
                        SpeciesName = "FireSwordMan",
                        Hp = 150,
                        Mp = 60*BATTLE_DYNAMICS_FPS, 
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(7.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
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
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_BULLWARRIOR, new CharacterConfig {
                        SpeciesId = SPECIES_BULLWARRIOR,
                        SpeciesName = "BullWarrior",
                        Hp = 500,
                        Mp = 60*BATTLE_DYNAMICS_FPS,
                        RepelSoftPushback = true,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 30,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 27,
                        Speed = (int)(1.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
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
                        DyingSizeY = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 3,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 8,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_GOBLIN, new CharacterConfig {
                        SpeciesId = SPECIES_GOBLIN,
                        SpeciesName = "Goblin",
                        Hp = 50,
                        Mp = 10*BATTLE_DYNAMICS_FPS,
                        InAirIdleFrameIdxTurningPoint = 1,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 12,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 4,
                        GetUpFramesToRecover = 12,
                        Speed = (int)(1.1f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(5.5f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
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
                        LayDownSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(12.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_MAGSWORDGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_MAGSWORDGIRL,
                        SpeciesName = "MagSwordGirl",
                        Hp = 120,
                        Mp = 30*BATTLE_DYNAMICS_FPS, // e.g. if (MpRegenRate == 1), then it takes 30 seconds to refill Mp from empty 
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 10,
                        GetUpFramesToRecover = 30,
                        DashingEnabled = true,
                        Speed = (int)(1.6f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(8.9 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        InertiaFramesToRecover = 8,
                        VisionOffsetX = (int)(8.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(16.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(100.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(32.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(36.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(36.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 3,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        SlidingEnabled = true,
                        CrouchingEnabled = true,
                        CrouchingAtkEnabled = true,
                        ProactiveJumpStartupFrames = 3,
                        UseInventoryBtnB = true,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        DefaultAirDashQuota = 3, // Her air dash is an attack, thus should be quite limited
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedMagazineIv,
                                Quota = 1,
                                FramesToRecover = 0,
                                DefaultQuota = 1,
                                DefaultFramesToRecover = 20*BATTLE_DYNAMICS_FPS,
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                                SkillId = 21, // TODO: Remove this hardcoded "skillId"!
                            },
                            new InventorySlot {
                                StockType = InventorySlotStockType.DummyIv,
                                Quota = 0,
                                FramesToRecover = 0,
                                DefaultQuota = 0,
                                DefaultFramesToRecover = MAX_INT, 
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                            },
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedMagazineIv,
                                Quota = 36,
                                FramesToRecover = 0,
                                DefaultQuota = 36,
                                DefaultFramesToRecover = 4*BATTLE_DYNAMICS_FPS, 
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                                SkillId = INVENTORY_BTN_B_SKILL,
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_SKELEARCHER, new CharacterConfig {
                        SpeciesId = SPECIES_SKELEARCHER,
                        SpeciesName = "SkeleArcher",
                        Hp = 50,
                        Mp = 60*BATTLE_DYNAMICS_FPS,
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
                        DashingEnabled = false,
                        OnWallEnabled = false,
                        VisionOffsetX = (int)(8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(220.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(30.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(30.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(30.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 3,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                    }),
            });
    }
}
