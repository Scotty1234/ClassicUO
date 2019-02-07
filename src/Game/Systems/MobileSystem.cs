//using Microsoft.Xna.Framework;

//using ClassicUO.Game.Data;
//using ClassicUO.Game.GameObjects;
//using ClassicUO.Input;
//using ClassicUO.IO;
//using ClassicUO.Renderer;
//using ClassicUO.Utility;
//using static ClassicUO.Game.GameObjects.Mobile;
//using ClassicUO.IO.Resources;
//using System.Collections.Generic;
//using System;

//namespace ClassicUO.Game.Systems
//{
//    struct MobileAnimation
//    {
//        public sbyte AnimIndex;
//        public byte AnimationFrameCount;
//        public byte AnimationInterval;
//        public byte AnimationRepeatMode;
//        public bool AnimationDirection;
//        public bool AnimationRepeat;
//        public bool AnimationFromServer;
//        public long LastAnimationIdleDelay;
//        public byte AnimationGroup;
//        public long LastAnimationChangeTime;
//    }

//    struct MobileComponent
//    {
//        public ushort Hits;
//        public ushort HitsMax;
//        public bool IsDead;
//        public bool IsRenamble;
//        public bool IsSAPoisoned;
//        public ushort Mana;
//        public ushort ManaMax;
//        public NotorietyFlag NotorietyFlag;
//        public RaceType Race;
//        public ushort Stamina;
//        public ushort StaminaMax;
//        public bool IsMounted;
//        public Deque<Step> Steps;
//        public CharacterSpeedType   SpeedMode;
//        public bool                 IsFemale;
//        public bool                 IsHuman;
//        public Item[]               Equipment;
//        public Hue                  Hue;
//        public Graphic              Graphic;
//        public Serial               Serial;
//        public bool                 IsWalking;
//        public bool                 IsRunning;
//        public bool                 IsFlying;
//        public bool                 InWarMode;
//        public Direction            Direction;

//    }

//    struct Position
//    {
//        public ushort X;
//        public ushort Y;
//    }

//    struct MobileViewLayer
//    {
//        public Hue  Hue;
//        public uint Hash;
//        public bool IsPartial;
//        public int  OffsetY;
//    }

//    struct MobileView
//    {
//        public MobileViewLayer[]    Frames;
//        public int                  LayerCount;
//        public bool                 IsFlipped;
//    }

//    sealed class MobileSystem
//    {
//        private MobileAnimation[]   _anims;
//        private MobileComponent[]   _mobileComponents;
//        private MobileView[]        _views;
//        private Position[]          _positions;
//        private int                 _numMobiles;

//        private static readonly byte[][] _animAssociateTable =
//{
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_WALK, (byte) HIGHT_ANIMATION_GROUP.HAG_WALK, (byte) PEOPLE_ANIMATION_GROUP.PAG_WALK_UNARMED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_WALK, (byte) HIGHT_ANIMATION_GROUP.HAG_WALK, (byte) PEOPLE_ANIMATION_GROUP.PAG_WALK_ARMED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_RUN, (byte) HIGHT_ANIMATION_GROUP.HAG_FLY, (byte) PEOPLE_ANIMATION_GROUP.PAG_RUN_UNARMED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_RUN, (byte) HIGHT_ANIMATION_GROUP.HAG_FLY, (byte) PEOPLE_ANIMATION_GROUP.PAG_RUN_ARMED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_STAND, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_STAND
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_1, (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_1
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_2, (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_2
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_STAND, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_STAND_ONEHANDED_ATTACK
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_STAND, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_STAND_TWOHANDED_ATTACK
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_3, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_ONEHANDED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_UNARMED_1
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_UNARMED_2
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_3, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_TWOHANDED_DOWN
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_TWOHANDED_WIDE
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_TWOHANDED_JAB
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_WALK, (byte) HIGHT_ANIMATION_GROUP.HAG_WALK, (byte) PEOPLE_ANIMATION_GROUP.PAG_WALK_WARMODE
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_CAST_DIRECTED
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_3, (byte) PEOPLE_ANIMATION_GROUP.PAG_CAST_AREA
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_BOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_CROSSBOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_GET_HIT_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_GET_HIT
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_DIE_1, (byte) HIGHT_ANIMATION_GROUP.HAG_DIE_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_DIE_1
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_DIE_2, (byte) HIGHT_ANIMATION_GROUP.HAG_DIE_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_DIE_2
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_WALK, (byte) HIGHT_ANIMATION_GROUP.HAG_WALK, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_RIDE_SLOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_RUN, (byte) HIGHT_ANIMATION_GROUP.HAG_FLY, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_RIDE_FAST
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_STAND, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_STAND
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_ATTACK
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_ATTACK_BOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_ATTACK_CROSSBOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_ATTACK_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_SLAP_HORSE
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_TURN
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_WALK, (byte) HIGHT_ANIMATION_GROUP.HAG_WALK, (byte) PEOPLE_ANIMATION_GROUP.PAG_ATTACK_UNARMED_AND_WALK
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_EMOTE_BOW
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_EAT, (byte) HIGHT_ANIMATION_GROUP.HAG_STAND, (byte) PEOPLE_ANIMATION_GROUP.PAG_EMOTE_SALUTE
//            },
//            new[]
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_1, (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_3
//            }
//        };

//        public MobileSystem()
//        {
//            int size = 100;

//            _anims = new MobileAnimation[size];
//            _mobileComponents = new MobileComponent[size];
//            _views = new MobileView[size];
//            _positions = new Position[size];
//            _numMobiles = 0;
//        }

//        public void Draw(Batcher2D batcher, Vector3 position, MouseOverList objectList)
//        {
//            for (int i = 0; i < _numMobiles; i++)
//            {
//                bool mirror = false;
//                byte dir = (byte)GetDirectionForAnimation(i);
//                FileManager.Animations.GetAnimDirection(ref dir, ref mirror);
//                _views[i].IsFlipped = mirror;
//                SetupLayers(dir, i, out int mountOffset);

//                //    if (Graphic == 0)
//                //        return false;

//                //    AnimationFrameTexture bodyFrame = FileManager.Animations.GetTexture(_frames[0].Hash);

//                //    if (bodyFrame == null)
//                //        continue;

//                //    int drawCenterY = bodyFrame.CenterY;
//                //    int drawX;
//                //    int drawY = mountOffset + drawCenterY + (int)(Offset.Z / 4) - 22 - (int)(Offset.Y - Offset.Z - 3);

//                //    if (IsFlipped)
//                //        drawX = -22 + (int)Offset.X;
//                //    else
//                //        drawX = -22 - (int)Offset.X;

//                //    FrameInfo = Rectangle.Empty;
//                //    Rectangle rect = Rectangle.Empty;

//                //    Hue hue = 0, targetColor = 0;
//                //    if (Engine.Profile.Current.HighlightMobilesByFlags)
//                //    {
//                //        if (IsPoisoned)
//                //            hue = 0x0044;

//                //        if (IsParalyzed)
//                //            hue = 0x014C;

//                //        if (NotorietyFlag != NotorietyFlag.Invulnerable && IsYellowHits)
//                //            hue = 0x0030;
//                //    }

//                //    bool isAttack = Serial == World.LastAttack;
//                //    bool isUnderMouse = IsSelected && (TargetManager.IsTargeting || World.Player.InWarMode);
//                //    bool needHpLine = false;

//                //    if (this != World.Player && (isAttack || isUnderMouse || TargetManager.LastGameObject == this))
//                //    {
//                //        targetColor = Notoriety.GetHue(NotorietyFlag);

//                //        if (isAttack || this == TargetManager.LastGameObject)
//                //        {
//                //            if (TargetLineGump.TTargetLineGump?.Mobile != this)
//                //            {
//                //                if (TargetLineGump.TTargetLineGump == null || TargetLineGump.TTargetLineGump.IsDisposed)
//                //                {
//                //                    TargetLineGump.TTargetLineGump = new TargetLineGump();
//                //                    Engine.UI.Add(TargetLineGump.TTargetLineGump);
//                //                }
//                //                else
//                //                {
//                //                    TargetLineGump.TTargetLineGump.SetMobile(this);
//                //                }
//                //            }

//                //            needHpLine = true;
//                //        }

//                //        if (isAttack || isUnderMouse)
//                //            hue = targetColor;
//                //    }

//                //    for (int i = 0; i < _layerCount; i++)
//                //    {
//                //        ViewLayer vl = _frames[i];
//                //        AnimationFrameTexture frame = FileManager.Animations.GetTexture(vl.Hash);

//                //        if (frame.IsDisposed) continue;
//                //        int x = drawX + frame.CenterX;
//                //        int y = -drawY - (frame.Height + frame.CenterY) + drawCenterY - vl.OffsetY;

//                //        int yy = -(frame.Height + frame.CenterY + 3);
//                //        int xx = -frame.CenterX;

//                //        if (mirror)
//                //            xx = -(frame.Width - frame.CenterX);

//                //        if (xx < rect.X)
//                //            rect.X = xx;

//                //        if (yy < rect.Y)
//                //            rect.Y = yy;

//                //        if (rect.Width < xx + frame.Width)
//                //            rect.Width = xx + frame.Width;

//                //        if (rect.Height < yy + frame.Height)
//                //            rect.Height = yy + frame.Height;

//                //        Texture = frame;
//                //        Bounds = new Rectangle(x, -y, frame.Width, frame.Height);

//                //        if (Engine.Profile.Current.NoColorObjectsOutOfRange && Distance > World.ViewRange)
//                //            HueVector = new Vector3(0x038E, 1, HueVector.Z);
//                //        else
//                //            HueVector = ShaderHuesTraslator.GetHueVector(this.IsHidden ? 0x038E : hue == 0 ? vl.Hue : hue, vl.IsPartial, 0, false);
//                //        base.Draw(batcher, position, objectList);
//                //        Pick(frame, Bounds, position, objectList);
//                //    }

//                //    FrameInfo.X = Math.Abs(rect.X);
//                //    FrameInfo.Y = Math.Abs(rect.Y);
//                //    FrameInfo.Width = FrameInfo.X + rect.Width;
//                //    FrameInfo.Height = FrameInfo.Y + rect.Height;

//                //    //MessageOverHead(batcher, position, IsMounted ? 0 : -22);

//                //    if (needHpLine)
//                //    {
//                //        TargetLineGump.TTargetLineGump.BackgroudHue = targetColor;

//                //        if (IsPoisoned)
//                //            TargetLineGump.TTargetLineGump.HpHue = 63;
//                //        else if (IsYellowHits)
//                //            TargetLineGump.TTargetLineGump.HpHue = 53;

//                //        else
//                //            TargetLineGump.TTargetLineGump.HpHue = 90;
//                //    }

//                //    Engine.DebugInfo.MobilesRendered++;
//                //    return true;
//            }
//        }

//        public void Update(double totalMS, double frameMS)
//        {
//            // Game object system should be called before this.
//            //Debug.Assert(GameObjectSystem.CalledThisFrame);

//            for (int i = 0; i < _numMobiles; i++)
//            {
//                if (_anims[i].LastAnimationIdleDelay < Engine.Ticks)
//                {
//                    SetIdleAnimation(i);
//                }

//                ProcessAnimation(i);
//            }
//        }

//        public Direction GetDirectionForAnimation(int index)
//        { 
//            return _mobileComponents[index].Steps.Count > 0 ? (Direction)_mobileComponents[index].Steps.Front().Direction : _mobileComponents[index].Direction;
//        }

//        private static readonly byte[,] _animationIdle =
//{
//            {
//                (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_1, (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_2, (byte) LOW_ANIMATION_GROUP.LAG_FIDGET_1
//            },
//            {
//                (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_1, (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_2, (byte) HIGHT_ANIMATION_GROUP.HAG_FIDGET_1
//            },
//            {
//                (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_1, (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_2, (byte) PEOPLE_ANIMATION_GROUP.PAG_FIDGET_3
//            }
//        };

//        public void SetIdleAnimation(int index)
//        {
//            _anims[index].LastAnimationIdleDelay = CalculateRandomIdleTime();

//            if (!_mobileComponents[index].IsMounted)
//            {
//               _anims[index].AnimIndex = 0;
//               _anims[index].AnimationFrameCount = 0;
//               _anims[index].AnimationInterval = 1;
//               _anims[index].AnimationRepeatMode = 1;
//               _anims[index].AnimationDirection = true;
//               _anims[index].AnimationRepeat = false;
//               _anims[index].AnimationFromServer = true;

//                byte groupIndex = (byte)FileManager.Animations.GetGroupIndex(GetGraphicForAnimation(_mobileComponents[index].Graphic));

//                _anims[index].AnimationGroup =  _animationIdle[groupIndex - 1, RandomHelper.GetValue(0, 2)];
//            }
//        }

//        private long CalculateRandomIdleTime()
//        {
//            long lastAnimationIdleDelay = Engine.Ticks + (30000 + RandomHelper.GetValue(0, 30000));
//            return lastAnimationIdleDelay;
//        }

//        public Graphic GetGraphicForAnimation(Graphic graphic)
//        {
//            ushort g = graphic;

//            switch (g)
//            {
//                case 0x0192:
//                case 0x0193:

//                    {
//                        g -= 2;

//                        break;
//                    }
//            }

//            return g;
//        }

//        private void Pick(int index, SpriteTexture texture, Rectangle area, Vector3 drawPosition, MouseOverList list)
//        {
//            int x;

//            if (_mobileComponents[index].IsFlipped)
//                x = (int)drawPosition.X + area.X + 44 - list.MousePosition.X;
//            else
//                x = list.MousePosition.X - (int)drawPosition.X + area.X;
//            int y = list.MousePosition.Y - ((int)drawPosition.Y - area.Y);
//            throw new NotImplementedException();
//            //if (texture.Contains(x, y)) list.Add(this, drawPosition);
//        }

//        private void SetupLayers(byte dir, int mobileIndex, out int mountOffset)
//        {
//            _views[mobileIndex].LayerCount = 0;
//            mountOffset = 0;

//            if (_mobileComponents[mobileIndex].IsHuman)
//            {
//                for (int i = 0; i < Constants.USED_LAYER_COUNT; i++)
//                {
//                    Layer layer = LayerOrder.UsedLayers[dir, i];

//                    if (IsCovered(mobileIndex, layer))
//                        continue;

//                    if (layer == Layer.Invalid)
//                    {
//                        AddLayer(mobileIndex, dir, GetGraphicForAnimation(_mobileComponents[mobileIndex].Graphic), _mobileComponents[mobileIndex].Hue, ispartial: true);
//                    }
//                    else
//                    {
//                        Item item;

//                        if ((item = _mobileComponents[mobileIndex].Equipment[(int)layer]) != null)
//                        {
//                            if (layer == Layer.Mount)
//                            {
//                                Item mount = _mobileComponents[mobileIndex].Equipment[(int)Layer.Mount];

//                                if (mount != null)
//                                {
//                                    Graphic mountGraphic = item.GetGraphicForAnimation();

//                                    if (mountGraphic < Constants.MAX_ANIMATIONS_DATA_INDEX_COUNT)
//                                        mountOffset = FileManager.Animations.DataIndex[mountGraphic].MountedHeightOffset;
//                                    AddLayer(mobileIndex, dir, mountGraphic, mount.Hue, offsetY: mountOffset);
//                                }
//                            }
//                            else
//                            {
//                                if (item.ItemData.AnimID != 0)
//                                {
//                                    if (_mobileComponents[mobileIndex].IsDead && (layer == Layer.Hair || layer == Layer.Beard)) continue;
//                                    EquipConvData? convertedItem = null;
//                                    Graphic graphic = item.ItemData.AnimID;
//                                    Hue hue = item.Hue;

//                                    if (FileManager.Animations.EquipConversions.TryGetValue(item.Graphic, out Dictionary<ushort, EquipConvData> map))
//                                    {
//                                        if (map.TryGetValue(item.ItemData.AnimID, out EquipConvData data))
//                                        {
//                                            convertedItem = data;
//                                            graphic = data.Graphic;
//                                        }
//                                    }

//                                    AddLayer(mobileIndex, dir, graphic, hue, convertedItem, item.ItemData.IsPartialHue);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            else
//            {
//                AddLayer(mobileIndex, dir, _mobileComponents[mobileIndex].Graphic, _mobileComponents[mobileIndex].IsDead ? (Hue)0x0386 : _mobileComponents[mobileIndex].Hue);
//            }
//        }

//        private void AddLayer(int index, byte dir, Graphic graphic, Hue hue, EquipConvData? convertedItem = null, bool ispartial = false, int offsetY = 0)
//        {
//            byte animGroup = GetGroupForAnimation(index, graphic);
//            sbyte animIndex = _anims[index].AnimIndex;

//            /* bool isitting = false;
//            if (mobile.IsHuman && !mounted)
//            {
//                if ((FileManager.Animations.SittingValue = mobile.IsSitting) != 0)
//                {
//                    animGroup = (byte) (FileManager.Animations.Direction == 3 ? 25 : (byte) PEOPLE_ANIMATION_GROUP.PAG_STAND);
//                    animIndex = 0;

//                    isitting = true;
//                }
//            } */

//            FileManager.Animations.AnimID = graphic;
//            FileManager.Animations.AnimGroup = animGroup;
//            FileManager.Animations.Direction = dir;
//            ref AnimationDirection direction = ref FileManager.Animations.DataIndex[FileManager.Animations.AnimID].Groups[FileManager.Animations.AnimGroup].Direction[FileManager.Animations.Direction];

//            if ((direction.FrameCount == 0 || direction.FramesHashes == null) && !FileManager.Animations.LoadDirectionGroup(ref direction))
//                return;
//            direction.LastAccessTime = Engine.Ticks;
//            int fc = direction.FrameCount;
//            if (fc > 0 && animIndex >= fc) animIndex = 0;

//            if (animIndex < direction.FrameCount)
//            {
//                uint hash = direction.FramesHashes[animIndex];

//                if (hash == 0)
//                {
//                    return;
//                }

//                if (hue == 0)
//                {
//                    if (direction.Address != direction.PatchedAddress)
//                    {
//                        hue = FileManager.Animations.DataIndex[FileManager.Animations.AnimID].Color;
//                    }

//                    if (hue == 0 && convertedItem.HasValue)
//                    {
//                        hue = convertedItem.Value.Color;
//                    }
//                }

//                //_frames[_layerCount++] = new ViewLayer(graphic, hue, hash, ispartial, offsetY /*, isitting */);

//                ref var frame = ref _views[index].Frames[_views[index].LayerCount++];
//                frame.Hue = hue;
//                frame.Hash = hash;
//                frame.OffsetY = offsetY;
//                frame.IsPartial = ispartial;
//            }
//        }

//        private static void CorrectAnimationByAnimSequence(ANIMATION_GROUPS type, ushort graphic, ref byte result)
//        {
//            if (FileManager.Animations.IsReplacedByAnimationSequence(graphic, out byte t))
//            {

//                switch (type)
//                {
//                    case ANIMATION_GROUPS.AG_LOW:


//                        break;
//                    case ANIMATION_GROUPS.AG_HIGHT:

//                        if (result == 1)
//                        {
//                            result = 25;
//                            return;
//                        }

//                        break;
//                    case ANIMATION_GROUPS.AG_PEOPLE:
//                        if (result == 1)
//                        {
//                            result = result;
//                            return;
//                        }
//                        break;
//                }

//                if (result == 4) // people stand
//                    result = 25;
//                else if (
//                        result == 0 || // people walk un armed / high walk
//                        result == 1 || // walk armed / high stand
//                        result == 15)  // walk warmode
//                    result = 22; // 22
//                else if (
//                        result == 2 || // people run unarmed
//                        result == 3 || // people run armed
//                        result == 19)  // high fly
//                    result = 24;
//            }
//        }

//        private static void CorrectAnimationGroup(ushort graphic, ANIMATION_GROUPS group, ref byte animation)
//        {
//            if (group == ANIMATION_GROUPS.AG_LOW)
//            {
//                switch ((LOW_ANIMATION_GROUP)animation)
//                {
//                    case LOW_ANIMATION_GROUP.LAG_DIE_2:
//                        animation = (byte)LOW_ANIMATION_GROUP.LAG_DIE_1;

//                        break;
//                    case LOW_ANIMATION_GROUP.LAG_FIDGET_2:
//                        animation = (byte)LOW_ANIMATION_GROUP.LAG_FIDGET_1;

//                        break;
//                    case LOW_ANIMATION_GROUP.LAG_ATTACK_3:
//                    case LOW_ANIMATION_GROUP.LAG_ATTACK_2:
//                        animation = (byte)LOW_ANIMATION_GROUP.LAG_ATTACK_1;

//                        break;
//                }

//                if (!FileManager.Animations.AnimationExists(graphic, animation)) animation = (byte)LOW_ANIMATION_GROUP.LAG_STAND;
//            }
//            else if (group == ANIMATION_GROUPS.AG_HIGHT)
//            {
//                switch ((HIGHT_ANIMATION_GROUP)animation)
//                {
//                    case HIGHT_ANIMATION_GROUP.HAG_DIE_2:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_DIE_1;

//                        break;
//                    case HIGHT_ANIMATION_GROUP.HAG_FIDGET_2:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_FIDGET_1;

//                        break;
//                    case HIGHT_ANIMATION_GROUP.HAG_ATTACK_3:
//                    case HIGHT_ANIMATION_GROUP.HAG_ATTACK_2:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_ATTACK_1;

//                        break;
//                    case HIGHT_ANIMATION_GROUP.HAG_GET_HIT_3:
//                    case HIGHT_ANIMATION_GROUP.HAG_GET_HIT_2:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_GET_HIT_1;

//                        break;
//                    case HIGHT_ANIMATION_GROUP.HAG_MISC_4:
//                    case HIGHT_ANIMATION_GROUP.HAG_MISC_3:
//                    case HIGHT_ANIMATION_GROUP.HAG_MISC_2:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_MISC_1;

//                        break;
//                    case HIGHT_ANIMATION_GROUP.HAG_FLY:
//                        animation = (byte)HIGHT_ANIMATION_GROUP.HAG_WALK;

//                        break;
//                }

//                if (!FileManager.Animations.AnimationExists(graphic, animation)) animation = (byte)HIGHT_ANIMATION_GROUP.HAG_STAND;
//            }
//            else if (group == ANIMATION_GROUPS.AG_PEOPLE)
//            {
//                switch ((PEOPLE_ANIMATION_GROUP)animation)
//                {
//                    case PEOPLE_ANIMATION_GROUP.PAG_FIDGET_2:
//                    case PEOPLE_ANIMATION_GROUP.PAG_FIDGET_3:
//                        animation = (byte)PEOPLE_ANIMATION_GROUP.PAG_FIDGET_1;

//                        break;
//                }

//                if (!FileManager.Animations.AnimationExists(graphic, animation))
//                    animation = (byte)PEOPLE_ANIMATION_GROUP.PAG_STAND;
//            }
//        }

//        private void GetGroupForAnimation(ANIMATION_GROUPS group, ref byte animation)
//        {
//            if ((sbyte)group != 0 && animation < (byte)PEOPLE_ANIMATION_GROUP.PAG_ANIMATION_COUNT)
//            {
//                animation = _animAssociateTable[animation][(sbyte)group - 1];
//            }
//        }

//        private byte GetGroupForAnimation(int index, ushort checkGraphic = 0)
//        {
//            Graphic graphic = checkGraphic;

//            if (graphic == 0)
//            {
//                graphic = GetGraphicForAnimation(_mobileComponents[index].Graphic);
//            }

//            ANIMATION_GROUPS groupIndex = FileManager.Animations.GetGroupIndex(graphic);
//            byte result = _anims[index].AnimationGroup;

//            if (result != 0xFF && (_mobileComponents[index].Serial & 0x80000000) == 0 && (!_anims[index].AnimationFromServer || checkGraphic > 0))
//            {
//                GetGroupForAnimation(groupIndex, ref result);

//                if (!FileManager.Animations.AnimationExists(graphic, result))
//                {
//                    CorrectAnimationGroup(graphic, groupIndex, ref result);
//                }
//            }

//            bool isWalking = _mobileComponents[index].IsWalking;
//            bool isRun = _mobileComponents[index].IsRunning;

//            if (_mobileComponents[index].Steps.Count > 0)
//            {
//                isWalking = true;
//                isRun = _mobileComponents[index].Steps.Front().Run;
//            }

//            if (groupIndex == ANIMATION_GROUPS.AG_LOW)
//            {
//                if (isWalking)
//                {
//                    if (isRun)
//                        result = (byte)LOW_ANIMATION_GROUP.LAG_RUN;
//                    else
//                        result = (byte)LOW_ANIMATION_GROUP.LAG_WALK;
//                }
//                else if (_anims[index].AnimationGroup == 0xFF)
//                {
//                    result = (byte)LOW_ANIMATION_GROUP.LAG_STAND;
//                    _anims[index].AnimIndex = 0;
//                }
//            }
//            else if (groupIndex == ANIMATION_GROUPS.AG_HIGHT)
//            {
//                if (isWalking)
//                {
//                    result = (byte)HIGHT_ANIMATION_GROUP.HAG_WALK;

//                    if (isRun)
//                    {
//                        if (FileManager.Animations.AnimationExists(graphic, (byte)HIGHT_ANIMATION_GROUP.HAG_FLY))
//                            result = (byte)HIGHT_ANIMATION_GROUP.HAG_FLY;
//                    }
//                }
//                else if (_anims[index].AnimationGroup == 0xFF)
//                {
//                    result = (byte)HIGHT_ANIMATION_GROUP.HAG_STAND;
//                    _anims[index].AnimIndex = 0;
//                }

//                if (graphic == 151)
//                    result++;
//            }
//            else if (groupIndex == ANIMATION_GROUPS.AG_PEOPLE)
//            {
//                bool inWar = _mobileComponents[index].InWarMode;

//                if (isWalking)
//                {
//                    if (isRun)
//                    {
//                        if (_mobileComponents[index].IsMounted)
//                        {
//                        	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_RIDE_FAST;
//                        }
//                        else if (_mobileComponents[index].Equipment[(int)Layer.OneHanded] != null || _mobileComponents[index].Equipment[(int)Layer.TwoHanded] != null)
//                        {
//                        	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_RUN_ARMED;
//                        }
//                        else
//                        {
//	                        result = (byte)PEOPLE_ANIMATION_GROUP.PAG_RUN_UNARMED;
//                        }

//                        if (!_mobileComponents[index].IsHuman && !FileManager.Animations.AnimationExists(graphic, result))
//                        {
//                            if (_mobileComponents[index].IsMounted)
//                            {
//                             	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_RIDE_SLOW;
//                            }
//                            else if ((_mobileComponents[index].Equipment[(int)Layer.TwoHanded] != null || _mobileComponents[index].Equipment[(int)Layer.OneHanded] != null) && !_mobileComponents[index].IsDead)
//                            {
//                                if (inWar)
//                                {
//                                	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_WARMODE;
//                                }
//                                else
//                                {
//                                	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_ARMED;
//                                }
//                            }
//                            else if (inWar && !_mobileComponents[index].IsDead)
//                            {
//                            	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_WARMODE;
//                            }
//                            else
//                            {
//                            	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_UNARMED;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if (_mobileComponents[index].IsMounted)
//                            result = (byte)PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_RIDE_SLOW;
//                        else if ((_mobileComponents[index].Equipment[(int)Layer.OneHanded] != null || _mobileComponents[index].Equipment[(int)Layer.TwoHanded] != null) && !_mobileComponents[index].IsDead)
//                        {
//                            if (inWar)
//                                result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_WARMODE;
//                            else
//                                result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_ARMED;
//                        }
//                        else if (inWar && !_mobileComponents[index].IsDead)
//                            result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_WARMODE;
//                        else
//                            result = (byte)PEOPLE_ANIMATION_GROUP.PAG_WALK_UNARMED;
//                    }
//                }
//                else if (_anims[index].AnimationGroup == 0xFF)
//                {
//                    if (_mobileComponents[index].IsMounted)
//                     {
//                     	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_ONMOUNT_STAND;
//                     }
//                    else if (inWar && !_mobileComponents[index].IsDead)
//                    {
//                        if (_mobileComponents[index].Equipment[(int)Layer.OneHanded] != null)
//                        {
//                        	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_STAND_ONEHANDED_ATTACK;
//                        }
//                        else if (_mobileComponents[index].Equipment[(int)Layer.TwoHanded] != null)
//                        {
//                        	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_STAND_TWOHANDED_ATTACK;
//                        }
//                        else
//                        {
//                        	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_STAND_ONEHANDED_ATTACK;
//                        }
//                    }
//                    else
//                    {
//                    	result = (byte)PEOPLE_ANIMATION_GROUP.PAG_STAND;
//                    }

//                    _anims[index].AnimIndex = 0;
//                }

//                if (_mobileComponents[index].Race == RaceType.GARGOYLE)
//                {
//                    if (_mobileComponents[index].IsFlying)
//                    {
//                        if (result == 0 || result == 1)
//                            result = 62;
//                        else if (result == 2 || result == 3)
//                            result = 63;
//                        else if (result == 4)
//                            result = 64;
//                        else if (result == 6)
//                            result = 66;
//                        else if (result == 7 || result == 8)
//                            result = 65;
//                        else if (result >= 9 && result <= 11)
//                            result = 71;
//                        else if (result >= 12 && result <= 14)
//                            result = 72;
//                        else if (result == 15)
//                            result = 62;
//                        else if (result == 20)
//                            result = 77;
//                        else if (result == 31)
//                            result = 71;
//                        else if (result == 34)
//                            result = 78;
//                        else if (result >= 200 && result <= 259)
//                            result = 75;
//                        else if (result >= 260 && result <= 270) result = 75;

//                        return result;
//                    }
//                }
//            }

//            CorrectAnimationByAnimSequence(groupIndex, graphic, ref result);

//            return result;
//        }

//        private bool IsCovered(int index, Layer layer)
//        {
//            Item pants;
//            Item robe;
//            Item skirt;
//            Item tunic;

//            switch (layer)
//            {
//                case Layer.Shoes:
//                {
//                    pants = _mobileComponents[index].Equipment[(int)Layer.Pants];

//                    if (_mobileComponents[index].Equipment[(int)Layer.Legs] != null || pants != null && pants.Graphic == 0x1411)
//                        return true;
//                    else
//                    {
//                        robe = _mobileComponents[index].Equipment[(int)Layer.Robe];

//                        if (pants != null && (pants.Graphic == 0x0513 || pants.Graphic == 0x0514) || robe != null && robe.Graphic == 0x0504)
//                            return true;
//                    }
//                }
//                break;

//                case Layer.Pants:
//                {
//                    robe = _mobileComponents[index].Equipment[(int)Layer.Robe];
//                    pants = _mobileComponents[index].Equipment[(int)Layer.Pants];

//                    if (_mobileComponents[index].Equipment[(int)Layer.Legs] != null || robe != null && robe.Graphic == 0x0504)
//                        return true;

//                    if (pants != null && (pants.Graphic == 0x01EB || pants.Graphic == 0x03E5 || pants.Graphic == 0x03eB))
//                    {
//                        skirt = _mobileComponents[index].Equipment[(int)Layer.Skirt];

//                        if (skirt != null && skirt.Graphic != 0x01C7 && skirt.Graphic != 0x01E4)
//                            return true;

//                        if (robe != null && robe.Graphic != 0x0229 && (robe.Graphic <= 0x04E7 || robe.Graphic > 0x04EB))
//                            return true;
//                    }
//                }
//                break;

//                case Layer.Tunic:
//                {
//	                robe = _mobileComponents[index].Equipment[(int)Layer.Robe];
//	                tunic = _mobileComponents[index].Equipment[(int)Layer.Tunic];
	
//	                if (robe != null && robe.Graphic != 0)
//	                    return true;
//	                else if (tunic != null && tunic.Graphic == 0x0238)
//	                    return robe != null && robe.Graphic != 0x9985 && robe.Graphic != 0x9986;
//                }
//                break;

//                case Layer.Torso:
//                {
//	                robe = _mobileComponents[index].Equipment[(int)Layer.Robe];
	
//	                if (robe != null && robe.Graphic != 0 && robe.Graphic != 0x9985 && robe.Graphic != 0x9986)
//	                    return true;
//	                else
//	                {
//	                    tunic = _mobileComponents[index].Equipment[(int)Layer.Tunic];
//	                    Item torso = _mobileComponents[index].Equipment[(int)Layer.Torso];
	
//	                    if (tunic != null && tunic.Graphic != 0x1541 && tunic.Graphic != 0x1542)
//	                        return true;
	
//	                    if (torso != null && (torso.Graphic == 0x782A || torso.Graphic == 0x782B))
//	                        return true;
//	                }
//                }
//                break;

//                case Layer.Arms:
//                {
//	                robe = _mobileComponents[index].Equipment[(int)Layer.Robe];
	
//	                return robe != null && robe.Graphic != 0 && robe.Graphic != 0x9985 && robe.Graphic != 0x9986;
//                }

//                case Layer.Helmet:
//                case Layer.Hair:
//                {
//	                robe = _mobileComponents[index].Equipment[(int)Layer.Robe];
	
//	                if (robe != null)
//	                {
//	                    if (robe.Graphic > 0x3173)
//	                    {
//	                        if (robe.Graphic == 0x4B9D || robe.Graphic == 0x7816)
//	                            return true;
//	                    }
//	                    else
//	                    {
//	                        if (robe.Graphic <= 0x2687)
//	                        {
//	                            if (robe.Graphic < 0x2683)
//	                                return robe.Graphic >= 0x204E && robe.Graphic <= 0x204F;
	
//	                            return true;
//	                        }
	
//	                        if (robe.Graphic == 0x2FB9 || robe.Graphic == 0x3173)
//	                            return true;
//	                    }
//	                }
//                }
//                break;

//                case Layer.Skirt:
//                {
//                	skirt = _mobileComponents[index].Equipment[(int)Layer.Skirt];
//                }
//                break;
//            }

//            return false;
//        }

//        private void ProcessAnimation(int index)
//        {
//            byte dir = (byte)GetDirectionForAnimation(index);

//            if (_mobileComponents[index].Steps.Count > 0)
//            {
//                bool turnOnly;

//                do
//                {
//                    Step step = _mobileComponents[index].Steps.Front();

//                    if (_anims[index].AnimationFromServer)
//                    {
//                        SetAnimation(index, 0xFF);
//                    }

//                    int maxDelay = MovementSpeed.TimeToCompleteMovement(this, step.Run);
//                    int delay = (int)Engine.Ticks - (int)_mobileComponents[index].LastStepTime;
//                    bool removeStep = delay >= maxDelay;

//                    //if ((byte) Direction == step.Direction)
//                    if (_positions[index].X != step.X || _positions[index].Y != step.Y)
//                    {
//                        float framesPerTile = maxDelay / (float)Constants.CHARACTER_ANIMATION_DELAY;
//                        float frameOffset = delay / (float)Constants.CHARACTER_ANIMATION_DELAY;
//                        float x = frameOffset;
//                        float y = frameOffset;

//                        MovementSpeed.GetPixelOffset((byte)_mobileComponents[index].Direction, ref x, ref y, framesPerTile);
//                        _mobileComponents[index].Offset = new Vector3((sbyte)x, (sbyte)y, (int)((step.Z - Z) * frameOffset * (4.0f / framesPerTile)));

//                        turnOnly = false;
//                    }
//                    else
//                    {
//                        turnOnly = true;
//                        removeStep = true;
//                    }

//                    if (removeStep)
//                    {
//                        if (this == World.Player)
//                        {
//                            //if (Position.X != step.X || Position.Y != step.Y || Position.Z != step.Z)
//                            //{
//                            //}

//                            if (Position.Z - step.Z >= 22)
//                            {
//                                // oUCH!!!!
//                                AddOverhead(MessageType.Label, "Ouch!");
//                            }

//#if !JAEDAN_MOVEMENT_PATCH && !MOVEMENT2
//                            if (World.Player.Walker.StepInfos[World.Player.Walker.CurrentWalkSequence].Accepted)
//                            {
//                                int sequence = World.Player.Walker.CurrentWalkSequence + 1;

//                                if (sequence < World.Player.Walker.StepsCount)
//                                {
//                                    int count = World.Player.Walker.StepsCount - sequence;

//                                    for (int i = 0; i < count; i++)
//                                    {
//                                        World.Player.Walker.StepInfos[sequence - 1] = World.Player.Walker.StepInfos[sequence];
//                                        sequence++;
//                                    }
//                                }

//                                World.Player.Walker.StepsCount--;
//                            }
//                            else
//                                World.Player.Walker.CurrentWalkSequence++;
//#endif
//                        }

//                        Position = new Position((ushort)step.X, (ushort)step.Y, step.Z);
//                        AddToTile();
//                        Direction = (Direction)step.Direction;
//                        IsRunning = step.Run;
//                        Offset = Vector3.Zero;
//                        Steps.RemoveFromFront();
//                        CalculateRandomIdleTime();
//                        LastStepTime = Engine.Ticks;
//                        ProcessDelta();
//                    }
//                } while (Steps.Count != 0 && turnOnly);
//            }

//            ProcessFootstepsSound();

//            if (LastAnimationChangeTime < Engine.Ticks && !NoIterateAnimIndex())
//            {
//                sbyte frameIndex = AnimIndex;

//                if (AnimationFromServer && !AnimationDirection)
//                    frameIndex--;
//                else
//                    frameIndex++;
//                Graphic id = GetGraphicForAnimation();
//                int animGroup = GetGroupForAnimation(this, id);

//                if (animGroup == 64 || animGroup == 65)
//                {
//                    animGroup = InWarMode ? 65 : 64;
//                    AnimationGroup = (byte)animGroup;
//                }

//                Item mount = Equipment[(int)Layer.Mount];

//                if (mount != null)
//                {
//                    switch (animGroup)
//                    {
//                        case (byte)PEOPLE_ANIMATION_GROUP.PAG_FIDGET_1:
//                        case (byte)PEOPLE_ANIMATION_GROUP.PAG_FIDGET_2:
//                        case (byte)PEOPLE_ANIMATION_GROUP.PAG_FIDGET_3:
//                            id = mount.GetGraphicForAnimation();
//                            animGroup = GetGroupForAnimation(this, id);

//                            break;
//                    }
//                }

//                bool mirror = false;
//                FileManager.Animations.GetAnimDirection(ref dir, ref mirror);
//                int currentDelay = Constants.CHARACTER_ANIMATION_DELAY;

//                if (id < Constants.MAX_ANIMATIONS_DATA_INDEX_COUNT && dir < 5)
//                {
//                    ref AnimationDirection direction = ref FileManager.Animations.DataIndex[id].Groups[animGroup].Direction[dir];
//                    FileManager.Animations.AnimID = id;
//                    FileManager.Animations.AnimGroup = (byte)animGroup;
//                    FileManager.Animations.Direction = dir;
//                    if ((direction.FrameCount == 0 || direction.FramesHashes == null)) FileManager.Animations.LoadDirectionGroup(ref direction);

//                    if (direction.Address != 0 || direction.IsUOP)
//                    {
//                        direction.LastAccessTime = Engine.Ticks;
//                        int fc = direction.FrameCount;

//                        if (AnimationFromServer)
//                        {
//                            currentDelay += currentDelay * (AnimationInterval + 1);

//                            if (AnimationFrameCount <= 0)
//                                AnimationFrameCount = (byte)fc;
//                            else
//                                fc = AnimationFrameCount;

//                            if (AnimationDirection)
//                            {
//                                if (frameIndex >= fc)
//                                {
//                                    frameIndex = 0;

//                                    if (AnimationRepeat)
//                                    {
//                                        byte repCount = AnimationRepeatMode;

//                                        if (repCount == 2)
//                                        {
//                                            repCount--;
//                                            AnimationRepeatMode = repCount;
//                                        }
//                                        else if (repCount == 1) SetAnimation(0xFF);
//                                    }
//                                    else
//                                        SetAnimation(0xFF);
//                                }
//                            }
//                            else
//                            {
//                                if (frameIndex < 0)
//                                {
//                                    if (fc <= 0)
//                                        frameIndex = 0;
//                                    else
//                                        frameIndex = (sbyte)(fc - 1);

//                                    if (AnimationRepeat)
//                                    {
//                                        byte repCount = AnimationRepeatMode;

//                                        if (repCount == 2)
//                                        {
//                                            repCount--;
//                                            AnimationRepeatMode = repCount;
//                                        }
//                                        else if (repCount == 1) SetAnimation(0xFF);
//                                    }
//                                    else
//                                        SetAnimation(0xFF);
//                                }
//                            }
//                        }
//                        else
//                        {
//                            if (frameIndex >= fc) frameIndex = 0;
//                        }

//                        AnimIndex = frameIndex;
//                    }
//                }

//                LastAnimationChangeTime = Engine.Ticks + currentDelay;
//            }
//        }

//        public void SetAnimation(int index, byte id, byte interval = 0, byte frameCount = 0, byte repeatCount = 0, bool repeat = false, bool frameDirection = false)
//        {
//            _anims[index].AnimationGroup = id;
//            _anims[index].AnimIndex = 0;
//            _anims[index].AnimationInterval = interval;
//            _anims[index].AnimationFrameCount = frameCount;
//            _anims[index].AnimationRepeatMode = repeatCount;
//            _anims[index].AnimationRepeat = repeat;
//            _anims[index].AnimationDirection = frameDirection;
//            _anims[index].AnimationFromServer = false;
//            _anims[index].LastAnimationChangeTime = Engine.Ticks;
//            _anims[index].LastAnimationIdleDelay = CalculateRandomIdleTime();
//        }

//    }
//}
