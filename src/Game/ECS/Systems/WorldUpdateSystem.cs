using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Systems;
using Entitas;

namespace ClassicUO.Game.ECS.Systems
{
    class WorldUpdateSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _mobiles;
        readonly IGroup<GameEntity> _items;

        public WorldUpdateSystem(Contexts contexts)
        {
            var mobileMatcher = Matcher<GameEntity>.AllOf(GameComponentsLookup.Mobile);
            _mobiles = Contexts.SharedInstance.Game.GetGroup(mobileMatcher);

            var itemMatcher = Matcher<GameEntity>.AllOf(GameComponentsLookup.Item);
            _items = Contexts.SharedInstance.Game.GetGroup(itemMatcher);
        }

        public void Execute()
        {
            foreach(var mobile in _mobiles)
            {

            }

            foreach(var item in _items)
            {

            }
        }
    }
}
