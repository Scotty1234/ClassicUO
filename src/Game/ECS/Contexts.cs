using Entitas;

namespace ClassicUO.Game.ECS
{
    internal partial class Contexts : IContexts
    {
        public const string Id = "Id";

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

        public void InitializeEntityIndices()
        {
            Game.AddEntityIndex(new PrimaryEntityIndex<GameEntity, int>(
                Id,
                Game.GetGroup(GameMatcher.Id),
                (e, c) => ((c as IdComponent).Value)));
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

    internal static class ContextsExtensions
    {
        public static GameEntity GetEntityWithId(this GameContext context, int value)
        {
            var primaryEntityIndex = context.GetEntityIndex(Contexts.Id) as PrimaryEntityIndex<GameEntity, int>;
            return primaryEntityIndex.GetEntity(value);
        }
    }
}
