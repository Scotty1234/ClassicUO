using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class StretchedComponent : IComponent
    {
    }

    internal partial class GameEntity
    {
        public void AddStretched()
        {
            int index = GameComponentsLookup.Stretched;
            var component = CreateComponent<StretchedComponent>(index);
            AddComponent(index, component);
        }
    }
}
