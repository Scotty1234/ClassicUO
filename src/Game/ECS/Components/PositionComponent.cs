using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class PositionComponent : IComponent
    {
        public ushort   X;
        public ushort   Y;
        public sbyte    Z;
    }

    internal sealed partial class GameEntity
    {
        public PositionComponent Position
        {
            get
            {
                return (GetComponent(GameComponentsLookup.Position) as PositionComponent);
            }
            set
            {
                ReplaceComponent(GameComponentsLookup.Position, value);
            }
        }

    }

}
