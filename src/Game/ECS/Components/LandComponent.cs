using Entitas;

namespace ClassicUO.Game.Systems.Components
{
    internal sealed class LandComponent : IComponent
    {
    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcherLand;

        public static IMatcher<GameEntity> Land
        {
            get
            {
                if (_matcherLand == null)
                {
                    var matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.Land);
                    matcher.componentNames = GameComponentsLookup.ComponentNames;
                    _matcherLand = matcher;
                }

                return _matcherLand;
            }
        }
    }
}
