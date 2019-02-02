using ClassicUO.Game.Data;
using ClassicUO.Game.GameObjects;
using ClassicUO.Input;
using ClassicUO.IO;
using ClassicUO.Renderer;
using ClassicUO.Utility;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.Systems
{
    struct MobileComponent
    {
        public ushort Hits;
        public ushort HitsMax;
        public bool IsDead;
        public bool IsRenamble;
        public bool IsSAPoisoned;
        public ushort Mana;
        public ushort ManaMax;
        public NotorietyFlag NotorietyFlag;
        public RaceType Race;
        public ushort Stamina;
        public ushort StaminaMax;
        public long LastAnimationIdleDelay;
        public bool IsMounted;
    }

    struct MobileViewLayer
    {
        public Hue Hue;
        public uint Hash;
        public bool IsPartial;
        public int OffsetY;
    }

    struct MobileView
    {
        public MobileViewLayer[] Frames;
        public int LayerCount;
    }

    sealed class MobileSystem
    {
        MobileComponent[] mobileComponents;
        MobileView[]      mobileViews;
        int _numMobiles;

        public MobileSystem()
        {
            int size = 100;

            mobileComponents = new MobileComponent[size];
            mobileViews = new MobileView[size];
            _numMobiles = 0;
        }

        public void Draw(Batcher2D batcher, Vector3 position, MouseOverList objectList)
        {
            //for(int i = 0; i < _numMobiles; i++)
            //{
            //    bool mirror = false;
            //    byte dir = (byte)GetDirectionForAnimation(i);
            //    FileManager.Animations.GetAnimDirection(ref dir, ref mirror);
            //    IsFlipped = mirror;
            //    SetupLayers(dir, i, out int mountOffset);

            //    if (Graphic == 0)
            //        return false;

            //    AnimationFrameTexture bodyFrame = FileManager.Animations.GetTexture(_frames[0].Hash);

            //    if (bodyFrame == null)
            //        continue;

            //    int drawCenterY = bodyFrame.CenterY;
            //    int drawX;
            //    int drawY = mountOffset + drawCenterY + (int)(Offset.Z / 4) - 22 - (int)(Offset.Y - Offset.Z - 3);

            //    if (IsFlipped)
            //        drawX = -22 + (int)Offset.X;
            //    else
            //        drawX = -22 - (int)Offset.X;

            //    FrameInfo = Rectangle.Empty;
            //    Rectangle rect = Rectangle.Empty;

            //    Hue hue = 0, targetColor = 0;
            //    if (Engine.Profile.Current.HighlightMobilesByFlags)
            //    {
            //        if (IsPoisoned)
            //            hue = 0x0044;

            //        if (IsParalyzed)
            //            hue = 0x014C;

            //        if (NotorietyFlag != NotorietyFlag.Invulnerable && IsYellowHits)
            //            hue = 0x0030;
            //    }

            //    bool isAttack = Serial == World.LastAttack;
            //    bool isUnderMouse = IsSelected && (TargetManager.IsTargeting || World.Player.InWarMode);
            //    bool needHpLine = false;

            //    if (this != World.Player && (isAttack || isUnderMouse || TargetManager.LastGameObject == this))
            //    {
            //        targetColor = Notoriety.GetHue(NotorietyFlag);

            //        if (isAttack || this == TargetManager.LastGameObject)
            //        {
            //            if (TargetLineGump.TTargetLineGump?.Mobile != this)
            //            {
            //                if (TargetLineGump.TTargetLineGump == null || TargetLineGump.TTargetLineGump.IsDisposed)
            //                {
            //                    TargetLineGump.TTargetLineGump = new TargetLineGump();
            //                    Engine.UI.Add(TargetLineGump.TTargetLineGump);
            //                }
            //                else
            //                {
            //                    TargetLineGump.TTargetLineGump.SetMobile(this);
            //                }
            //            }

            //            needHpLine = true;
            //        }

            //        if (isAttack || isUnderMouse)
            //            hue = targetColor;
            //    }

            //    for (int i = 0; i < _layerCount; i++)
            //    {
            //        ViewLayer vl = _frames[i];
            //        AnimationFrameTexture frame = FileManager.Animations.GetTexture(vl.Hash);

            //        if (frame.IsDisposed) continue;
            //        int x = drawX + frame.CenterX;
            //        int y = -drawY - (frame.Height + frame.CenterY) + drawCenterY - vl.OffsetY;

            //        int yy = -(frame.Height + frame.CenterY + 3);
            //        int xx = -frame.CenterX;

            //        if (mirror)
            //            xx = -(frame.Width - frame.CenterX);

            //        if (xx < rect.X)
            //            rect.X = xx;

            //        if (yy < rect.Y)
            //            rect.Y = yy;

            //        if (rect.Width < xx + frame.Width)
            //            rect.Width = xx + frame.Width;

            //        if (rect.Height < yy + frame.Height)
            //            rect.Height = yy + frame.Height;

            //        Texture = frame;
            //        Bounds = new Rectangle(x, -y, frame.Width, frame.Height);

            //        if (Engine.Profile.Current.NoColorObjectsOutOfRange && Distance > World.ViewRange)
            //            HueVector = new Vector3(0x038E, 1, HueVector.Z);
            //        else
            //            HueVector = ShaderHuesTraslator.GetHueVector(this.IsHidden ? 0x038E : hue == 0 ? vl.Hue : hue, vl.IsPartial, 0, false);
            //        base.Draw(batcher, position, objectList);
            //        Pick(frame, Bounds, position, objectList);
            //    }

            //    FrameInfo.X = Math.Abs(rect.X);
            //    FrameInfo.Y = Math.Abs(rect.Y);
            //    FrameInfo.Width = FrameInfo.X + rect.Width;
            //    FrameInfo.Height = FrameInfo.Y + rect.Height;

            //    //MessageOverHead(batcher, position, IsMounted ? 0 : -22);

            //    if (needHpLine)
            //    {
            //        TargetLineGump.TTargetLineGump.BackgroudHue = targetColor;

            //        if (IsPoisoned)
            //            TargetLineGump.TTargetLineGump.HpHue = 63;
            //        else if (IsYellowHits)
            //            TargetLineGump.TTargetLineGump.HpHue = 53;

            //        else
            //            TargetLineGump.TTargetLineGump.HpHue = 90;
            //    }

            //    Engine.DebugInfo.MobilesRendered++;
            //    return true;
            //}
        }

        public void Update(double totalMS, double frameMS)
        {
            // Game object system should be called before this.
            //Debug.Assert(GameObjectSystem.CalledThisFrame);

            for (int i = 0; i < _numMobiles; i++)
            {
                if (mobileComponents[i].LastAnimationIdleDelay < Engine.Ticks)
                {
                    SetIdleAnimation(i);
                }

               // ProcessAnimation(i);
            }
        }

        public void SetIdleAnimation(int index)
        {
            mobileComponents[index].LastAnimationIdleDelay = CalculateRandomIdleTime();

            if (!mobileComponents[index].IsMounted)
            {
                //mobileComponents[index].AnimIndex = 0;
                //mobileComponents[index].AnimationFrameCount = 0;
                //mobileComponents[index].AnimationInterval = 1;
                //mobileComponents[index].AnimationRepeatMode = 1;
                //mobileComponents[index].AnimationDirection = true;
                //mobileComponents[index].AnimationRepeat = false;
                //mobileComponents[index].AnimationFromServer = true;

                //byte groupIndex = (byte)FileManager.Animations.GetGroupIndex(GetGraphicForAnimation());

                //AnimationGroup = _animationIdle[groupIndexdex - 1, RandomHelper.GetValue(0, 2)];
            }
        }

        private long CalculateRandomIdleTime()
        {
            long lastAnimationIdleDelay = Engine.Ticks + (30000 + RandomHelper.GetValue(0, 30000));
            return lastAnimationIdleDelay;
        }

        //private void Pick(int index, SpriteTexture texture, Rectangle area, Vector3 drawPosition, MouseOverList list)
        //{
        //    int x;

        //    if ( IsFlipped)
        //        x = (int)drawPosition.X + area.X + 44 - list.MousePosition.X;
        //    else
        //        x = list.MousePosition.X - (int)drawPosition.X + area.X;
        //    int y = list.MousePosition.Y - ((int)drawPosition.Y - area.Y);
        //    if (texture.Contains(x, y)) list.Add(this, drawPosition);
        //}
    }
}
