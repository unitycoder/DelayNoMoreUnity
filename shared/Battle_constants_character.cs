using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static shared.CharacterState;

namespace shared {
    public partial class Battle {
        public const int SPECIES_NONE_CH = -1;
        public const int SPECIES_KNIFEGIRL = 0;
        public const int SPECIES_SWORDMAN = 1;
        public const int SPECIES_MONKGIRL = 2;
        public const int SPECIES_FIRESWORDMAN = 3;
        public const int SPECIES_GUNGIRL = 4;
        public const int SPECIES_SUPERKNIFEGIRL = 5;

        public const int SPECIES_BULLWARRIOR = 4096;
        public const int SPECIES_GOBLIN = 4097;

        public const float DEFAULT_MIN_FALLING_VEL_Y_COLLISION_SPACE = -4.5f; 
        public const int DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID = (int)(DEFAULT_MIN_FALLING_VEL_Y_COLLISION_SPACE*COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO); 

        /**
         [WARNING] 

         The "SizeY" component of "BlownUp/LayDown/Dying" MUST be smaller or equal to that of "Shrinked", such that when a character is blown up an falled onto a "slip-jump provider", it wouldn't trigger an unexpected slip-jump.
         */
        public static ImmutableDictionary<int, CharacterConfig> characters = ImmutableDictionary.Create<int, CharacterConfig>().AddRange(
                new[]
                {
                    new KeyValuePair<int, CharacterConfig>(SPECIES_KNIFEGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_KNIFEGIRL,
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
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(48.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedIv,
                                Quota = 1,
                                FramesToRecover = 0,
                                DefaultQuota = 1,
                                DefaultFramesToRecover = 1800,
                                BuffSpeciesId = XformToSuperKnifeGirl.SpeciesId,
                                SkillId = NO_SKILL,
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_SWORDMAN, new CharacterConfig {
                        SpeciesId = SPECIES_SWORDMAN,
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
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_MONKGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_MONKGIRL,
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
                        DefaultSizeX = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(46.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(46.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(46.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(28.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedIv,
                                Quota = 1,
                                FramesToRecover = 0,
                                DefaultQuota = 1,
                                DefaultFramesToRecover = 720,
                                BuffSpeciesId = ShortFreezer.SpeciesId,
                                SkillId = NO_SKILL,
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_FIRESWORDMAN, new CharacterConfig {
                        SpeciesId = SPECIES_FIRESWORDMAN,
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
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(44.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = false,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_GUNGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_GUNGIRL,
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
                        LayDownSizeX = (int)(36.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(36.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        SlidingEnabled = true,
                        CrouchingEnabled = true,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID,
                        InitInventorySlots = new List<InventorySlot> {
                            new InventorySlot {
                                StockType = InventorySlotStockType.TimedMagazineIv,
                                Quota = 5,
                                FramesToRecover = 0,
                                DefaultQuota = 5,
                                DefaultFramesToRecover = 720,
                                BuffSpeciesId = TERMINATING_BUFF_SPECIES_ID,
                                SkillId = 27, // TODO: Remove this hardcoded "skillId"!
                            }
                        }
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_BULLWARRIOR, new CharacterConfig {
                        SpeciesId = SPECIES_BULLWARRIOR,
                        SpeciesName = "BullWarrior",
                        Hp = 500,
                        RepelSoftPushback = true,
                        InAirIdleFrameIdxTurningPoint = 11,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 16,
                        LayDownFramesToRecover = 30,
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
                        DyingSizeY = (int)(80 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 2,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 8,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_GOBLIN, new CharacterConfig {
                        SpeciesId = SPECIES_GOBLIN,
                        SpeciesName = "Goblin",
                        Hp = 50,
                        InAirIdleFrameIdxTurningPoint = 1,
                        InAirIdleFrameIdxTurnedCycle = 1,
                        LayDownFrames = 12,
                        LayDownFramesToRecover = 16,
                        GetUpInvinsibleFrames = 4,
                        GetUpFramesToRecover = 12,
                        Speed = (int)(1.1f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DownSlopePrimerVelY = (int)(-0.8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        JumpingInitVelY = (int)(4 * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
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
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 4,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),

                    new KeyValuePair<int, CharacterConfig>(SPECIES_SUPERKNIFEGIRL, new CharacterConfig {
                        SpeciesId = SPECIES_SUPERKNIFEGIRL,
                        SpeciesName = "SuperKnifeGirl",
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
                        OnWallEnabled = false,
                        VisionOffsetX = (int)(8f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionOffsetY = (int)(24f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeX = (int)(160.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        VisionSizeY = (int)(80.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DefaultSizeY = (int)(50.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeX = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        ShrinkedSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeX = (int)(48.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        LayDownSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeX = (int)(48.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        DyingSizeY = (int)(24.0f * COLLISION_SPACE_TO_VIRTUAL_GRID_RATIO),
                        MpRegenRate = 1,
                        CollisionTypeMask = COLLISION_CHARACTER_INDEX_PREFIX,
                        HasTurnAroundAnim = true,
                        ProactiveJumpStartupFrames = 2,
                        Hardness = 5,
                        MinFallingVelY = DEFAULT_MIN_FALLING_VEL_Y_VIRTUAL_GRID  
                    }),
            });
    }
}
