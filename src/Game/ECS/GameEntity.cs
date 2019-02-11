using Entitas;

namespace ClassicUO.Game.Systems
{
    internal sealed partial class GameEntity : Entitas.Entity
    {
        public T CreateAndAddComponent<T>(int index) where T : IComponent, new()
        {
            T component = CreateComponent<T>(index);
            AddComponent(index, component);
            return component;
        }
    }
}
