using System.Collections.Generic;

using Entitas;
using Microsoft.Xna.Framework;

using ClassicUO.Game.Map;

namespace ClassicUO.Game.ECS
{
    internal sealed class MapComponent : IComponent
    {
        public bool[] _blockAccessList = new bool[0x1000];
        public List<int> UsedIndices = new List<int>();
        public int Index;
        public Chunk[] Chunks;
        public int MapBlockIndex;
        public Point Center;
    }

    internal partial class GameEntity
    {
        public void AddMap()
        {
            var component = CreateComponent<MapComponent>(GameComponentsLookup.Map);
            AddComponent(GameComponentsLookup.Map, component);
        }

        public bool HasMap => HasComponent(GameComponentsLookup.Map);
        public MapComponent Map => (GetComponent(GameComponentsLookup.Map) as MapComponent);

        public void RemoveMap()
        {
            RemoveComponent(GameComponentsLookup.Map);
        }

    }

    internal sealed partial class GameMatcher
    {
        static IMatcher<GameEntity> _matcherMap;

        public static IMatcher<GameEntity> Map
        {
            get
            {
                if (_matcherMap == null)
                {
                    var matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.Map);
                    matcher.componentNames = GameComponentsLookup.ComponentNames;
                    _matcherMap = matcher;
                }

                return _matcherMap;
            }
        }
    }
}
