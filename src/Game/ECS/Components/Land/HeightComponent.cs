using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class HeightComponent : IComponent
    {
        public sbyte Average;
        public sbyte Minimum;
    }

    internal partial class GameEntity
    {
        public void AddHeight(sbyte average, sbyte minimum)
        {
            int index = GameComponentsLookup.Height;
            var component = CreateComponent<HeightComponent>(index);
            component.Average = average;
            component.Minimum = minimum;
            AddComponent(index, component);
        }
    }
}
