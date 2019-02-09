using Entitas;

namespace ClassicUO.Game.Systems
{
    internal partial class Contexts : IContexts
    {
        public static Contexts SharedInstance
        {
            get
            {
                if (_sharedInstance == null)
                {
                    _sharedInstance = new Contexts();
                }

                return _sharedInstance;
            }
            set { _sharedInstance = value; }
        }

        static Contexts _sharedInstance;

        public GameContext Game { get; set; }

        public IContext[] allContexts { get { return new IContext[] { Game,}; } }

        public Contexts()
        {
            Game = new GameContext();
        }

        public void Reset()
        {
            var contexts = allContexts;

            for (int i = 0; i < contexts.Length; i++)
            {
                contexts[i].Reset();
            }
        }
    }
}
