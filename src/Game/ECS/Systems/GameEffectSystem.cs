
namespace ClassicUO.Game.ECS
{
    public struct GameEffect
    {
        public int SourceX;
        public int SourceY;
        public int SourceZ;

        public int TargetX;
        public int TargetY;
        public int TargetZ;

        public int Speed;
        public bool IsEnabled;

        public long Duration;

    }

    sealed class GameEffectSystem
    {
        GameEffect[] GameEffects;

        public GameEffectSystem()
        {
            GameEffects = new GameEffect[100];
        }

        public void Update(double totalMS, double frameMS)
        {
            // Needs GO update first.

            for(int i = 0; i < GameEffects.Length; i++)
            {
               
            }
        }
    }
}
