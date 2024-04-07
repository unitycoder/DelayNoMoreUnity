﻿using System.Collections.Generic;

namespace shared {
    public struct CharacterConfig {
        // [WARNING] All fields of this class should be deemed as "initial values", and could be changed during a battle by whatever powerup/debuff :)
        public int SpeciesId;
        public string SpeciesName;

        public int Hp;
        public int Mp;

        public int InAirIdleFrameIdxTurningPoint;
        public int InAirIdleFrameIdxTurnedCycle;

        public int LayDownFrames;
        public int LayDownFramesToRecover;

        public int GetUpInvinsibleFrames;
        public int GetUpFramesToRecover;

        public int Speed;
        public int DownSlopePrimerVelY; // this value should be big enough such that smooth transition is enabled for the steepest slope, but small enough such that no character would penetrate the thinnest barrier
        public int MpRegenRate; // an integer for mp regeneration rate PER FRAME

        public int JumpingInitVelY;
        public int InertiaFramesToRecover;
        public bool DashingEnabled;
        public bool SlidingEnabled;
        public bool OnWallEnabled;
        public bool CrouchingEnabled;
        public int WallJumpingFramesToRecover;
        public int WallJumpingInitVelX;
        public int WallJumpingInitVelY;
        public int WallSlidingVelY;
        public int MinFallingVelY;

        public bool UseInventoryBtnB;

        // Collision boxes
        public int VisionOffsetX;
        public int VisionOffsetY;
        public int VisionSizeX;
        public int VisionSizeY;

        public int DefaultSizeX;
        public int DefaultSizeY;

        public int ShrinkedSizeX;
        public int ShrinkedSizeY;

        public int LayDownSizeX;
        public int LayDownSizeY;

        public int DyingSizeX;
        public int DyingSizeY;

        // Collision masks
        public bool OmitGravity;
        public bool OmitSoftPushback;
        public bool RepelSoftPushback;
        public ulong CollisionTypeMask;

        public bool HasTurnAroundAnim;

        public int Hardness;

        public int ProactiveJumpStartupFrames;
        
        public int DefaultAirJumpQuota;
        public int DefaultAirDashQuota;

        public bool IsolatedAirJumpAndDashQuota; // default is false, in most cases AirJump and AirDash quotas are deduced together (but default initial quota can be different) 

        public List<InventorySlot> InitInventorySlots;
    }
}
