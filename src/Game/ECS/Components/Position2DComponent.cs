using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class Position2DComponent : IComponent
    {
        public ushort X;
        public ushort Y;
    }

    internal partial class GameEntity
    {
        public void AddPosition2D(ushort x, ushort y)
        {
            var component = CreateComponent<Position2DComponent>(GameComponentsLookup.Position2D);
            component.X = x;
            component.Y = y;
            AddComponent(GameComponentsLookup.Position2D, component);
        }
    }
}
