
namespace ClassicUO.Game.Systems
{
    struct MobileAnimation
    {    
        public sbyte AnimIndex;
        public byte AnimationFrameCount;
        public byte AnimationInterval;
        public byte AnimationRepeatMode;
        public bool AnimationDirection ;
        public bool AnimationRepeat;
        public bool AnimationFromServer;
    }

    sealed class MobileAnimationSystem
    {
        MobileAnimation[] mobileAnimations;

        //public byte GetGroupForAnimation(ushort checkGraphic = 0)
        //{

        //}
    }
}
