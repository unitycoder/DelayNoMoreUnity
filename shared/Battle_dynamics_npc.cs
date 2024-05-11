using System;
using Google.Protobuf.Collections;
using static shared.CharacterState;

namespace shared {
    public partial class Battle {
        private static int OPPONENT_REACTION_UNKNOWN = 0;
        private static int OPPONENT_REACTION_USE_MELEE = 1;
        private static int OPPONENT_REACTION_USE_DRAGONPUNCH = 1;
        private static int OPPONENT_REACTION_USE_FIREBALL = 3;
        private static int OPPONENT_REACTION_FOLLOW = 4;
        //private static int OPPONENT_REACTION_FOLLOW_ASAP = 5;
        private static int OPPONENT_REACTION_NOT_ENOUGH_MP = 6;
        private static int OPPONENT_REACTION_FLEE = 7;
        //private static int OPPONENT_REACTION_FLEE_ASAP = 8;

        private static int frontOpponentReachableByDragonPunch(CharacterDownsync currCharacterDownsync, Collider aCollider, Collider bCollider, float absColliderDx, float colliderDy, float absColliderDy, CharacterConfig opponentChConfig) {
            int yfac = (0 < colliderDy ? 1 : -1);
            float boxCx, boxCy, boxCwHalf, boxChHalf;
            bool closeEnough = false;
            // No need to calculate the exact bounding box of opponent based on ChState, this is just an estimation.
            switch (currCharacterDownsync.SpeciesId) {
                case SPECIES_SWORDMAN:
                    if (currCharacterDownsync.Mp < SwordManDragonPunchPrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(SwordManDragonPunchPrimerBullet.HitboxOffsetX + (opponentChConfig.ShrinkedSizeX >> 1), yfac * SwordManDragonPunchPrimerBullet.HitboxOffsetY + (opponentChConfig.ShrinkedSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((SwordManDragonPunchPrimerBullet.HitboxSizeX >> 1), (SwordManDragonPunchPrimerBullet.HitboxSizeY >> 1));
                    closeEnough = (boxCx + boxCwHalf >= 0.5f*absColliderDx) && (0.1f*aCollider.H < colliderDy && boxCy + boxChHalf > 0.2f*colliderDy); 
                    break;
                case SPECIES_FIRESWORDMAN:
                    if (currCharacterDownsync.Mp < FireSwordManDragonPunchPrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(FireSwordManDragonPunchPrimerBullet.HitboxOffsetX + (opponentChConfig.ShrinkedSizeX >> 1), yfac * FireSwordManDragonPunchPrimerBullet.HitboxOffsetY + (opponentChConfig.ShrinkedSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((FireSwordManDragonPunchPrimerBullet.HitboxSizeX >> 1), (FireSwordManDragonPunchPrimerBullet.HitboxSizeY >> 1));
                    closeEnough = (boxCx + boxCwHalf >= 0.5f*absColliderDx) && (0.1f*aCollider.H < colliderDy && boxCy + boxChHalf > 0.2f*colliderDy); 
                    break;
                case SPECIES_SKELEARCHER:
                    if (currCharacterDownsync.Mp < PurpleArrowRainSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    closeEnough = (0 < colliderDy && absColliderDy > 0.3f * (bCollider.H-aCollider.H));
                    break;
                default:
                    return OPPONENT_REACTION_UNKNOWN;
            }
            if (closeEnough) {
                return OPPONENT_REACTION_USE_DRAGONPUNCH;
            } else {
                return OPPONENT_REACTION_FOLLOW;
            }
        }

        private static int frontOpponentReachableByMelee1(CharacterDownsync currCharacterDownsync, Collider aCollider, Collider bCollider, float absColliderDx, float colliderDy, float absColliderDy, CharacterConfig opponentChConfig) {
            int yfac = (0 < colliderDy ? 1 : -1);
            float boxCx, boxCy, boxCwHalf, boxChHalf;
            switch (currCharacterDownsync.SpeciesId) {
                case SPECIES_SWORDMAN:
                    if (currCharacterDownsync.Mp < SwordManMelee1PrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(SwordManMelee1PrimerBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * SwordManMelee1PrimerBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((SwordManMelee1PrimerBullet.HitboxSizeX >> 1), (SwordManMelee1PrimerBullet.HitboxSizeY >> 1));
                    break;
                case SPECIES_FIRESWORDMAN:
                    if (currCharacterDownsync.Mp < FireSwordManMelee1PrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(FireSwordManMelee1PrimerBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * FireSwordManMelee1PrimerBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((FireSwordManMelee1PrimerBullet.HitboxSizeX >> 1), (FireSwordManMelee1PrimerBullet.HitboxSizeY >> 1));
                    break;
                case SPECIES_BULLWARRIOR:
                    if (currCharacterDownsync.Mp < BullWarriorMelee1PrimarySkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(BullWarriorMelee1PrimaryBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * BullWarriorMelee1PrimaryBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((BullWarriorMelee1PrimaryBullet.HitboxSizeX >> 1), (BullWarriorMelee1PrimaryBullet.HitboxSizeY >> 1));
                    break;
                case SPECIES_GOBLIN:
                    if (currCharacterDownsync.Mp < GoblinMelee1PrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(GoblinMelee1PrimerBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * GoblinMelee1PrimerBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((GoblinMelee1PrimerBullet.HitboxSizeX >> 1), (GoblinMelee1PrimerBullet.HitboxSizeY >> 1));
                    break;
                case SPECIES_SKELEARCHER:
                    if (currCharacterDownsync.Mp < PurpleArrowPrimarySkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(PurpleArrowBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * PurpleArrowBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((PurpleArrowBullet.HitboxSizeX >> 2), (PurpleArrowBullet.HitboxSizeY >> 2));
                    break;
                case SPECIES_BAT:
                case SPECIES_FIREBAT:
                    if (currCharacterDownsync.Mp < BatMelee1PrimerSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;         
                    (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(BatMelee1PrimerBullet.HitboxOffsetX + (opponentChConfig.DefaultSizeX >> 1), yfac * BatMelee1PrimerBullet.HitboxOffsetY + (opponentChConfig.DefaultSizeY >> 1));
                    (boxCwHalf, boxChHalf) = VirtualGridToPolygonColliderCtr((BatMelee1PrimerBullet.HitboxSizeX >> 1), (BatMelee1PrimerBullet.HitboxSizeY >> 1));
                    break;
                default:
                    return OPPONENT_REACTION_UNKNOWN;
            }
            // By now must have enough MP for the corresponding skill
            bool closeEnough = (boxCx + boxCwHalf >= absColliderDx) && (boxCy + boxChHalf >= absColliderDy);
            if (closeEnough) {
                return OPPONENT_REACTION_USE_MELEE;
            } else {
                return OPPONENT_REACTION_FOLLOW;
            }
        }

        private static int frontOpponentReachableByFireball(CharacterDownsync currCharacterDownsync, Collider aCollider, Collider bCollider, float colliderDx, float colliderDy, CharacterConfig opponentChConfig) {
            // Whenever there's an opponent in vision, it's deemed already close enough for fireball
            switch (currCharacterDownsync.SpeciesId) {
                case SPECIES_FIRESWORDMAN:
                    if (currCharacterDownsync.Mp < FireSwordManFireballSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;
                    break;
                case SPECIES_BULLWARRIOR:
                    if (currCharacterDownsync.Mp < BullWarriorFireballSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;
                    break;
                case SPECIES_SKELEARCHER:
                    if (currCharacterDownsync.Mp < PurpleArrowRainSkill.MpDelta) return OPPONENT_REACTION_NOT_ENOUGH_MP;
                    break;
                default:
                    return OPPONENT_REACTION_UNKNOWN;
            }
            return OPPONENT_REACTION_USE_FIREBALL;
        }

        private static void findHorizontallyClosestCharacterCollider(CharacterDownsync currCharacterDownsync, Collider aCollider, Collision collision, ref SatResult overlapResult, out Collider? res1, out CharacterDownsync? res2) {
            res1 = null;
            res2 = null;

            // [WARNING] Finding only the closest non-self character to react to for avoiding any randomness. 
            bool collided = aCollider.CheckAllWithHolder(0, 0, collision, COLLIDABLE_PAIRS);
            if (!collided) return;

            float minAbsColliderDx = MAX_FLOAT32;
            float minAbsColliderDy = MAX_FLOAT32;

            ConvexPolygon aShape = aCollider.Shape;
            while (true) {
                var (ok3, bCollider) = collision.PopFirstContactedCollider();
                if (false == ok3 || null == bCollider) {
                    break;
                }

                CharacterDownsync? v3 = bCollider.Data as CharacterDownsync;
                if (null == v3) {
                    // Only check shape collision (which is relatively expensive) if it's CharacterDownsync 
                    continue;
                }

                if (v3.JoinIndex == currCharacterDownsync.JoinIndex || v3.BulletTeamId == currCharacterDownsync.BulletTeamId) {
                    continue;
                }

                if (invinsibleSet.Contains(v3.CharacterState) || 0 < v3.FramesInvinsible) continue; // Target is invinsible, nothing can be done

                ConvexPolygon bShape = bCollider.Shape;
                var (overlapped, _, _) = calcPushbacks(0, 0, aShape, bShape, false, false, ref overlapResult);
                if (!overlapped) {
                    continue;
                }

                // By now we're sure that it should react to the PatrolCue
                var colliderDx = (aCollider.X - bCollider.X);
                var absColliderDx = Math.Abs(colliderDx);
                if (absColliderDx > minAbsColliderDx) {
                    continue;
                }

                var colliderDy = (aCollider.Y - bCollider.Y);
                var absColliderDy = Math.Abs(colliderDy);
                if (absColliderDx == minAbsColliderDx && absColliderDy > minAbsColliderDy) {
                    continue;
                }
                minAbsColliderDx = absColliderDx;
                minAbsColliderDy = absColliderDy;
                res1 = bCollider;
                res2 = v3;
            }
        }

        private static int countNpcI(RepeatedField<CharacterDownsync> npcsArr) {
            int npcI = 0;
            while (npcI < npcsArr.Count && TERMINATING_PLAYER_ID != npcsArr[npcI].Id) npcI++;
            return npcI;
        }

        public static bool isNpcDeadToDisappear(CharacterDownsync currCharacterDownsync) {
            return (0 >= currCharacterDownsync.Hp && 0 >= currCharacterDownsync.FramesToRecover);
        }

        public static bool isNpcJustDead(CharacterDownsync currCharacterDownsync) {
            return (0 >= currCharacterDownsync.Hp && DYING_FRAMES_TO_RECOVER == currCharacterDownsync.FramesToRecover);
        }

        private static void findHorizontallyClosestPatrolCueCollider(CharacterDownsync currCharacterDownsync, Collider aCollider, Collision collision, ref SatResult overlapResult, out Collider? res1, out PatrolCue? res2) {
            res1 = null;
            res2 = null;

            // [WARNING] Finding only the closest patrol cue to react to for avoiding any randomness. 
            bool collided = aCollider.CheckAllWithHolder(0, 0, collision, COLLIDABLE_PAIRS);
            if (!collided) return;

            float minAbsColliderDx = MAX_FLOAT32;
            float minAbsColliderDy = MAX_FLOAT32;

            ConvexPolygon aShape = aCollider.Shape;
            while (true) {
                var (ok3, bCollider) = collision.PopFirstContactedCollider();
                if (false == ok3 || null == bCollider) {
                    break;
                }

                PatrolCue? v3 = bCollider.Data as PatrolCue;
                if (null == v3) {
                    // Only check shape collision (which is relatively expensive) if it's PatrolCue 
                    continue;
                }

                ConvexPolygon bShape = bCollider.Shape;
                var (overlapped, _, _) = calcPushbacks(0, 0, aShape, bShape, false, false, ref overlapResult);
                if (!overlapped) {
                    continue;
                }

                // By now we're sure that it should react to the PatrolCue
                var colliderDx = (aCollider.X - bCollider.X);
                var absColliderDx = Math.Abs(colliderDx);
                if (absColliderDx > minAbsColliderDx) {
                    continue;
                }

                var colliderDy = (aCollider.Y - bCollider.Y);
                var absColliderDy = Math.Abs(colliderDy);
                if (absColliderDx == minAbsColliderDx && absColliderDy > minAbsColliderDy) {
                    continue;
                }
                minAbsColliderDx = absColliderDx;
                minAbsColliderDy = absColliderDy;
                res1 = bCollider;
                res2 = v3;
            }
        }

        private static (int, bool, bool, bool, int, int) deriveNpcOpPattern(CharacterDownsync currCharacterDownsync, RoomDownsyncFrame currRenderFrame, int roomCapacity, CharacterConfig chConfig, CharacterDownsync thatCharacterInNextFrame, Collider[] dynamicRectangleColliders, int colliderCnt, CollisionSpace collisionSys, Collision collision, ref SatResult overlapResult, InputFrameDecoded decodedInputHolder, ILoggerBridge logger) {
            //This function returns (patternId, jumpedOrNot, slipJumpedOrNot, effectiveDx, effectiveDy)
            
            //return (PATTERN_ID_UNABLE_TO_OP, false, false, 0, 0);

            if (0 < currCharacterDownsync.FramesToRecover) {
                return (PATTERN_ID_UNABLE_TO_OP, false, false, false, 0, 0);
            }

            bool interrupted = _processDebuffDuringInput(currCharacterDownsync);
            if (interrupted) {
                return (PATTERN_ID_UNABLE_TO_OP, false, false, false, 0, 0);
            }

            int patternId = PATTERN_ID_NO_OP;
            bool jumpedOrNot = false;
            bool slipJumpedOrNot = false;
            bool jumpHolding = false;

            // By default keeps the movement aligned with current facing
            int effectiveDx = currCharacterDownsync.DirX;
            int effectiveDy = currCharacterDownsync.DirY;
            if (CharacterState.InAirIdle1ByJump == currCharacterDownsync.CharacterState || CharacterState.InAirIdle1ByWallJump == currCharacterDownsync.CharacterState || CharacterState.InAirIdle2ByJump == currCharacterDownsync.CharacterState) {
                jumpHolding = true;
            } 

            int visionReaction = OPPONENT_REACTION_UNKNOWN;
            var aCollider = dynamicRectangleColliders[currCharacterDownsync.JoinIndex - 1]; // already added to collisionSys

            float visionCx, visionCy, visionCw, visionCh;
            calcNpcVisionBoxInCollisionSpace(currCharacterDownsync, chConfig, out visionCx, out visionCy, out visionCw, out visionCh);

            var visionCollider = dynamicRectangleColliders[colliderCnt];
            UpdateRectCollider(visionCollider, visionCx, visionCy, visionCw, visionCh, SNAP_INTO_PLATFORM_OVERLAP, SNAP_INTO_PLATFORM_OVERLAP, SNAP_INTO_PLATFORM_OVERLAP, SNAP_INTO_PLATFORM_OVERLAP, 0, 0, currCharacterDownsync, COLLISION_VISION_INDEX_PREFIX);
            collisionSys.AddSingle(visionCollider);

            Collider? bCollider;
            CharacterDownsync? v3;
            findHorizontallyClosestCharacterCollider(currCharacterDownsync, visionCollider, collision, ref overlapResult, out bCollider, out v3);

            float bColliderDx = 0f, bColliderDy = 0f;
            if (null != bCollider && null != v3) {
                bColliderDx = (bCollider.X - aCollider.X);
                bColliderDy = (bCollider.Y - aCollider.Y);
            
                var atkedChConfig = characters[v3.SpeciesId];
                float absColliderDx = Math.Abs(bColliderDx), absColliderDy = Math.Abs(bColliderDy);

                bool opponentBehindMe = (0 > (bColliderDx * currCharacterDownsync.DirX)); 
                if (!opponentBehindMe) {
                    // Opponent is in front of me
                    int s1 = frontOpponentReachableByDragonPunch(currCharacterDownsync, aCollider, bCollider, absColliderDx, bColliderDy, absColliderDy, atkedChConfig); // [WARNING] When just transited from GetUp1 to Idle1, dragonpunch might be triggered due to the delayed virtualGridY bouncing back. 
                    int s2 = frontOpponentReachableByMelee1(currCharacterDownsync, aCollider, bCollider, absColliderDx, bColliderDy, absColliderDy, atkedChConfig);
                    int s3 = frontOpponentReachableByFireball(currCharacterDownsync, aCollider, bCollider, bColliderDx, bColliderDy, atkedChConfig); 
                    if (OPPONENT_REACTION_NOT_ENOUGH_MP == s1 && OPPONENT_REACTION_NOT_ENOUGH_MP == s2 && OPPONENT_REACTION_NOT_ENOUGH_MP == s3) {
                        visionReaction = OPPONENT_REACTION_FLEE;   
                    } else if (OPPONENT_REACTION_USE_DRAGONPUNCH == s1) {
                        patternId = PATTERN_UP_B;
                        visionReaction = s1;
                    } else if (OPPONENT_REACTION_USE_MELEE == s2) {
                        patternId = PATTERN_B;
                        visionReaction = s2;
                    } else if (OPPONENT_REACTION_USE_FIREBALL == s3) {
                        patternId = PATTERN_DOWN_B;
                        visionReaction = s3;
                    } else {
                        visionReaction = OPPONENT_REACTION_FOLLOW;
                    }
                } else {
                    // Opponent is behind me
                    bool farEnough = (absColliderDx > 0.5f*(aCollider.W+bCollider.W)); // To avoid bouncing turn-arounds
                    if (farEnough) {
                        visionReaction = OPPONENT_REACTION_FOLLOW;
                    }
                }

                if (OPPONENT_REACTION_FOLLOW == visionReaction) {
                    if (currCharacterDownsync.OmitGravity || chConfig.OmitGravity) { 
                        var magSqr = bColliderDx * bColliderDx + bColliderDy * bColliderDy;
                        var invMag = InvSqrt32(magSqr);

                        float normX = bColliderDx*invMag, normY = bColliderDy*invMag;
                        if (opponentBehindMe) {
                            normX = -normX;
                        }
                        var (effDx, effDy, _) = DiscretizeDirection(normX, normY);
                        effectiveDx = effDx;
                        effectiveDy = effDy;
                    } else {
                        if (opponentBehindMe) {
                            effectiveDx = -effectiveDx;
                        }
                    }
                } else if (OPPONENT_REACTION_FLEE == visionReaction) {
                    if (currCharacterDownsync.OmitGravity || chConfig.OmitGravity) { 
                        var magSqr = bColliderDx * bColliderDx + bColliderDy * bColliderDy;
                        var invMag = InvSqrt32(magSqr);

                        float normX = -bColliderDx*invMag, normY = -bColliderDy*invMag;
                        var (effDx, effDy, _) = DiscretizeDirection(normX, normY);
                        effectiveDx = effDx;
                        effectiveDy = effDy;
                    } else {
                        if (opponentBehindMe) {
                            // DO NOTHING, just continue walking
                        }
                    }
                }
            }

            collisionSys.RemoveSingle(visionCollider); // no need to increment "colliderCnt", the visionCollider is transient
            visionCollider.Data = null;

            if (OPPONENT_REACTION_UNKNOWN != visionReaction && PATTERN_ID_NO_OP != patternId) {
                // [WARNING] Even if there were no vision reation, if "PATTERN_ID_NO_OP == patternId", we still expect the NPC to make use of patrol cues to jump or turn around!
                jumpHolding = false;
                thatCharacterInNextFrame.CapturedByPatrolCue = false;
                thatCharacterInNextFrame.FramesInPatrolCue = 0;
                thatCharacterInNextFrame.WaivingPatrolCueId = NO_PATROL_CUE_ID;
            } else if (!noOpSet.Contains(currCharacterDownsync.CharacterState)) {
                bool hasPatrolCueReaction = false;
                // [WARNING] The field "CharacterDownsync.FramesInPatrolCue" would also be re-purposed as "patrol cue collision waiving frames" by the logic here.
                Collider? pCollider;
                PatrolCue? v4;
                findHorizontallyClosestPatrolCueCollider(currCharacterDownsync, aCollider, collision, ref overlapResult, out pCollider, out v4); 
                
                if (null != pCollider && null != v4) {
                    bool prevCapturedByPatrolCue = currCharacterDownsync.CapturedByPatrolCue;

                    // By now we're sure that it should react to the PatrolCue
                    var colliderDx = (aCollider.X - pCollider.X);
                    var colliderDy = (aCollider.Y - pCollider.Y);
                    bool fr = 0 < colliderDx && (0 > currCharacterDownsync.VelX || (0 == currCharacterDownsync.VelX && 0 > currCharacterDownsync.DirX));
                    bool fl = 0 > colliderDx && (0 < currCharacterDownsync.VelX || (0 == currCharacterDownsync.VelX && 0 < currCharacterDownsync.DirX));
                    bool fu = 0 < colliderDy && (0 > currCharacterDownsync.VelY || (0 == currCharacterDownsync.VelY && 0 > currCharacterDownsync.DirY));
                    bool fd = 0 > colliderDy && (0 < currCharacterDownsync.VelY || (0 == currCharacterDownsync.VelY && 0 < currCharacterDownsync.DirY));
                    if (!fr && !fl && !fu && !fd) {
                        fr = 0 > currCharacterDownsync.DirX;
                        fl = 0 < currCharacterDownsync.DirX;
                    }

                    int targetFramesInPatrolCue = 0;
                    if (fr) {
                        targetFramesInPatrolCue = (int)v4.FrCaptureFrames;
                        DecodeInput(v4.FrAct, decodedInputHolder);
                        //logger.LogInfo(String.Format("aCollider={{ X:{0}, Y:{1}, W:{2}, H:{3} }} collided with pCollider={{ X:{4}, Y:{5}, W:{6}, H:{7}, cue={8} }} from the right", aCollider.X, aCollider.Y, aCollider.W, aCollider.H, pCollider.X, pCollider.Y, pCollider.W, pCollider.H, v4)); 
                    } else if (fl) {
                        targetFramesInPatrolCue = (int)v4.FlCaptureFrames;
                        DecodeInput(v4.FlAct, decodedInputHolder);
                        //logger.LogInfo(String.Format("aCollider={{ X:{0}, Y:{1}, W:{2}, H:{3} }} collided with pCollider={{ X:{4}, Y:{5}, W:{6}, H:{7}, cue={8} }} from the left", aCollider.X, aCollider.Y, aCollider.W, aCollider.H, pCollider.X, pCollider.Y, pCollider.W, pCollider.H, v4));
                    } else if (fu) {
                        targetFramesInPatrolCue = (int)v4.FuCaptureFrames;
                        DecodeInput(v4.FuAct, decodedInputHolder);
                        //logger.LogInfo(String.Format("aCollider={{ X:{0}, Y:{1}, W:{2}, H:{3} }} collided with pCollider={{ X:{4}, Y:{5}, W:{6}, H:{7}, cue={8} }} from the top", aCollider.X, aCollider.Y, aCollider.W, aCollider.H, pCollider.X, pCollider.Y, pCollider.W, pCollider.H, v4)); 
                    } else if (fd) {
                        targetFramesInPatrolCue = (int)v4.FdCaptureFrames;
                        DecodeInput(v4.FdAct, decodedInputHolder);
                        //logger.LogInfo(String.Format("aCollider={{ X:{0}, Y:{1}, W:{2}, H:{3} }} collided with pCollider={{ X:{4}, Y:{5}, W:{6}, H:{7}, cue={8} }} from the bottom", aCollider.X, aCollider.Y, aCollider.W, aCollider.H, pCollider.X, pCollider.Y, pCollider.W, pCollider.H, v4)); 
                    } else {
                        logger.LogWarn(String.Format("aCollider={{ X:{0}, Y:{1}, W:{2}, H:{3}, dirX: {9} }} collided with pCollider={{ X:{4}, Y:{5}, W:{6}, H:{7}, cue={8} }} but direction couldn't be determined!", aCollider.X, aCollider.Y, aCollider.W, aCollider.H, pCollider.X, pCollider.Y, pCollider.W, pCollider.H, v4, currCharacterDownsync.DirX));
                    }

                    bool shouldWaivePatrolCueReaction = (false == prevCapturedByPatrolCue && 0 < currCharacterDownsync.FramesInPatrolCue && v4.Id == currCharacterDownsync.WaivingPatrolCueId && (0 == decodedInputHolder.BtnALevel && 0 == decodedInputHolder.BtnBLevel)); // Don't waive if the cue contains an action button (i.e. BtnA or BtnB)

                    do {
                        if (shouldWaivePatrolCueReaction) {
                            // [WARNING] It's difficult to move this "early return" block to be before the "shape overlap check" like that of the traps, because we need data from "decodedInputHolder". 
                            // logger.LogInfo(String.Format("rdf.Id={0}, Npc joinIndex={1}, speciesId={2} is waived for patrolCueId={3} because it has (prevCapturedByPatrolCue={4}, framesInPatrolCue={5}, waivingPatrolCueId={6}).", currRenderFrame.Id, currCharacterDownsync.JoinIndex, currCharacterDownsync.SpeciesId, v4.Id, prevCapturedByPatrolCue, currCharacterDownsync.FramesInPatrolCue, currCharacterDownsync.WaivingPatrolCueId));
                            break;
                        }

                        bool shouldBreakPatrolCueCapture = ((true == prevCapturedByPatrolCue) && (currCharacterDownsync.WaivingPatrolCueId == v4.Id) && (0 == currCharacterDownsync.FramesInPatrolCue));

                        bool shouldEnterCapturedPeriod = ((false == prevCapturedByPatrolCue) && (false == shouldBreakPatrolCueCapture) && (0 < targetFramesInPatrolCue));

                        if (shouldEnterCapturedPeriod) {
                            thatCharacterInNextFrame.CapturedByPatrolCue = true;
                            thatCharacterInNextFrame.FramesInPatrolCue = targetFramesInPatrolCue;
                            thatCharacterInNextFrame.WaivingPatrolCueId = v4.Id;
                            effectiveDx = 0;
                            effectiveDy = 0;
                            jumpedOrNot = false;
                            jumpHolding = false;
                            slipJumpedOrNot = false;
                            hasPatrolCueReaction = true;
                            break;
                        }

                        bool isReallyCaptured = ((true == prevCapturedByPatrolCue) && (false == shouldBreakPatrolCueCapture) && (v4.Id == currCharacterDownsync.WaivingPatrolCueId) && (0 < currCharacterDownsync.FramesInPatrolCue));
                        if (isReallyCaptured) {
                            effectiveDx = 0;
                            effectiveDy = 0;
                            jumpedOrNot = false;
                            jumpHolding = false;
                            slipJumpedOrNot = false;
                            hasPatrolCueReaction = true;
                        } else {
                            effectiveDx = decodedInputHolder.Dx;
                            effectiveDy = decodedInputHolder.Dy;
                            slipJumpedOrNot = (0 == currCharacterDownsync.FramesToRecover) && (currCharacterDownsync.PrimarilyOnSlippableHardPushback && 0 < decodedInputHolder.Dy && 0 == decodedInputHolder.Dx) && (0 < decodedInputHolder.BtnALevel);
                            jumpedOrNot = !slipJumpedOrNot && (0 == currCharacterDownsync.FramesToRecover) && !inAirSet.Contains(currCharacterDownsync.CharacterState) && (0 < decodedInputHolder.BtnALevel);
                            jumpHolding = !slipJumpedOrNot && (0 == currCharacterDownsync.FramesToRecover) && (0 < decodedInputHolder.BtnALevel);
                            hasPatrolCueReaction = true;
                            thatCharacterInNextFrame.CapturedByPatrolCue = false;
                            thatCharacterInNextFrame.FramesInPatrolCue = DEFAULT_PATROL_CUE_WAIVING_FRAMES; // re-purposed
                            thatCharacterInNextFrame.WaivingPatrolCueId = v4.Id;
                        }
                    } while (false);
                }

                if (OPPONENT_REACTION_UNKNOWN == visionReaction && false == hasPatrolCueReaction && (currCharacterDownsync.WaivingSpontaneousPatrol || MAGIC_EVTSUB_ID_NONE != currCharacterDownsync.SubscriptionId)) {
                    return (PATTERN_ID_UNABLE_TO_OP, false, false, false, 0, 0);
                }
            }

            return (patternId, jumpedOrNot, slipJumpedOrNot, jumpHolding, effectiveDx, effectiveDy);
        }

        private static void _processNpcInputs(RoomDownsyncFrame currRenderFrame, int roomCapacity, int npcCnt, RepeatedField<CharacterDownsync> nextRenderFrameNpcs, RepeatedField<Bullet> nextRenderFrameBullets, Collider[] dynamicRectangleColliders, int colliderCnt, Collision collision, CollisionSpace collisionSys, ref SatResult overlapResult, InputFrameDecoded decodedInputHolder, ref int bulletLocalIdCounter, ref int bulletCnt, ILoggerBridge logger) {
            for (int i = roomCapacity; i < roomCapacity + npcCnt; i++) {
                var currCharacterDownsync = currRenderFrame.NpcsArr[i - roomCapacity];
                if (TERMINATING_PLAYER_ID == currCharacterDownsync.Id) break;
                var thatCharacterInNextFrame = nextRenderFrameNpcs[i - roomCapacity];
                var chConfig = characters[currCharacterDownsync.SpeciesId];
                var (patternId, jumpedOrNot, slipJumpedOrNot, jumpHolding, effDx, effDy) = deriveNpcOpPattern(currCharacterDownsync, currRenderFrame, roomCapacity, chConfig, thatCharacterInNextFrame, dynamicRectangleColliders, colliderCnt, collisionSys, collision, ref overlapResult, decodedInputHolder, logger);

                if (PATTERN_ID_UNABLE_TO_OP == patternId && 0 < currCharacterDownsync.FramesToRecover) {
                    _processNextFrameJumpStartup(currRenderFrame.Id, currCharacterDownsync, thatCharacterInNextFrame, chConfig, logger);
                    _processDelayedBulletSelfVel(currRenderFrame.Id, currCharacterDownsync, thatCharacterInNextFrame, chConfig, logger);
                    continue;
                }

                thatCharacterInNextFrame.JumpTriggered = jumpedOrNot;
                thatCharacterInNextFrame.SlipJumpTriggered = slipJumpedOrNot;
                thatCharacterInNextFrame.JumpHolding = jumpHolding;

                bool usedSkill = _useSkill(patternId, currCharacterDownsync, chConfig, thatCharacterInNextFrame, ref bulletLocalIdCounter, ref bulletCnt, currRenderFrame, nextRenderFrameBullets, false, logger);
                Skill? skillConfig = null;
                if (usedSkill) {
                    skillConfig = skills[thatCharacterInNextFrame.ActiveSkillId];
                    if (CharacterState.Dashing == skillConfig.BoundChState && currCharacterDownsync.InAir) {              
                        thatCharacterInNextFrame.RemainingAirDashQuota -= 1;
                        if (!chConfig.IsolatedAirJumpAndDashQuota) {
                            thatCharacterInNextFrame.RemainingAirJumpQuota -= 1;
                            if (0 > thatCharacterInNextFrame.RemainingAirJumpQuota) {
                                thatCharacterInNextFrame.RemainingAirJumpQuota = 0;
                            }
                        }
                    }
                    if (isCrouching(currCharacterDownsync.CharacterState) && CharacterState.Atk1 == thatCharacterInNextFrame.CharacterState) {
                        if (chConfig.CrouchingAtkEnabled) {
                            thatCharacterInNextFrame.CharacterState = CharacterState.CrouchAtk1;
                        }
                    }
                    continue; // Don't allow movement if skill is used
                } 
                    
                _processNextFrameJumpStartup(currRenderFrame.Id, currCharacterDownsync, thatCharacterInNextFrame, chConfig, logger);
                if (!currCharacterDownsync.OmitGravity && !chConfig.OmitGravity) {
                    _processInertiaWalking(currRenderFrame.Id, currCharacterDownsync, thatCharacterInNextFrame, effDx, effDy, chConfig, true, true, skillConfig, logger);
                } else {
                    _processInertiaFlying(currRenderFrame.Id, currCharacterDownsync, thatCharacterInNextFrame, effDx, effDy, chConfig, true, true, skillConfig, logger);
                    if (PATTERN_ID_UNABLE_TO_OP != patternId && chConfig.AntiGravityWhenIdle && Walking == thatCharacterInNextFrame.CharacterState && chConfig.AntiGravityFramesLingering < thatCharacterInNextFrame.FramesInChState) {
                        thatCharacterInNextFrame.CharacterState = CharacterState.InAirIdle1NoJump;
                        thatCharacterInNextFrame.FramesInChState = 0;
                    }
                }
            }
        }

        public static void calcNpcVisionBoxInCollisionSpace(CharacterDownsync characterDownsync, CharacterConfig chConfig, out float boxCx, out float boxCy, out float boxCw, out float boxCh) {
            if (noOpSet.Contains(characterDownsync.CharacterState)) {
                (boxCx, boxCy) = (0, 0);
                (boxCw, boxCh) = (0, 0);
                return;
            }

            int xfac = (0 < characterDownsync.DirX ? 1 : -1);
            (boxCx, boxCy) = VirtualGridToPolygonColliderCtr(characterDownsync.VirtualGridX + xfac * chConfig.VisionOffsetX, characterDownsync.VirtualGridY + chConfig.VisionOffsetY);
            (boxCw, boxCh) = VirtualGridToPolygonColliderCtr(chConfig.VisionSizeX, chConfig.VisionSizeY);
        }
    }
}
