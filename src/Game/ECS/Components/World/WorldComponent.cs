using Entitas;

using ClassicUO.Game.Managers;

namespace ClassicUO.Game.ECS
{
    internal sealed class WorldComponent : IComponent
    {
        public int MapIndex;
        public PartyManager Party;
        public int PlayerEntityID;
        public string ServerName;
        public byte ViewRange;
    }

    internal partial class GameEntity
    {
        public void AddWorld()
        {

        }
    }

    internal partial class GameContext
    {
        public GameEntity WorldEntity
        {
            get
            {
                return GetGroup(GameMatcher.World).GetSingleEntity();
            }
        }
    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcherWorld;

        public static IMatcher<GameEntity> World
        {
            get
            {
                if (_matcherWorld == null)
                {
                    var matcher = Matcher<GameEntity>.AllOf(GameComponentsLookup.World);
                    _matcherWorld = matcher;
                }

                return _matcherWorld;
            }
        }
    }
}
