using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class PlayerComponent : IComponent
    {
    }

    internal partial class GameContext
    {
        public GameEntity PlayerEntity
        {
            get
            {
                return GetGroup(GameMatcher.Player).GetSingleEntity();
            }
        }
    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcherPlayer;

        public static IMatcher<GameEntity> Player
        {
            get
            {
                if (_matcherMap == null)
                {
                    var matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.Player);
                    matcher.componentNames = GameComponentsLookup.ComponentNames;
                    _matcherPlayer = matcher;
                }

                return _matcherPlayer;
            }
        }
    }
}
