using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class MapIndexComponent : IComponent
    {
        public int Value;
    }

    internal partial class GameEntity
    {
        public void AddMapIndex(int value)
        {
            var component = CreateComponent<MapIndexComponent>(GameComponentsLookup.MapIndex);
            component.Value = value;
            AddComponent(GameComponentsLookup.MapIndex, component);
        }

        public MapIndexComponent MapIndex => (GetComponent(GameComponentsLookup.MapIndex) as MapIndexComponent);

        public void ReplaceMapIndex(int value)
        {
            var component = CreateComponent<MapIndexComponent>(GameComponentsLookup.MapIndex);
            component.Value = value;
            ReplaceComponent(GameComponentsLookup.MapIndex, component);
        }
    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcher;

        public static IMatcher<GameEntity> MapIndex
        {
            get
            {
                if (_matcher == null)
                {
                    var matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.MapIndex);
                    matcher.componentNames = GameComponentsLookup.ComponentNames;
                    _matcher = matcher;
                }

                return _matcher;
            }
        }
    }
}
