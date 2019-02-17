using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class IdComponent : IComponent
    {
        public int Value;
    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcherId;

        public static IMatcher<GameEntity> Id
        {
            get
            {
                if (_matcherId == null)
                {
                    var matcher = (Entitas.Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.Id);
                    matcher.componentNames = GameComponentsLookup.ComponentNames;
                    _matcherId = matcher;
                }

                return _matcherId;
            }
        }
    }
}
