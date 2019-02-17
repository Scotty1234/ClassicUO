using System.Collections.Generic;

using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class UsedIndicesComponent : IComponent
    {
        public List<int> UsedIndices;
    }

    internal partial class GameEntity
    {
        public UsedIndicesComponent UsedIndices
        {
            get
            {
                return (GetComponent(GameComponentsLookup.UsedIndices) as UsedIndicesComponent);
            }
        }

        public void ReplaceUsedIndices(List<int> newValue)
        {
            var index = GameComponentsLookup.UsedIndices;
            var component = CreateComponent<UsedIndicesComponent>(index);
            component.UsedIndices = newValue;
            ReplaceComponent(index, component);
        }
    }
}
