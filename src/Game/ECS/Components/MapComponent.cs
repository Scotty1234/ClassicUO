using ClassicUO.Game.Map;
using ClassicUO.Game.Systems;
using Entitas;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ClassicUO.Game.ECS.Components
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
