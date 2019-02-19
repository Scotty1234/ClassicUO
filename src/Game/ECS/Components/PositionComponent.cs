using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class PositionComponent : IComponent
    {
        public Position Value;
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

        public void ReplacePosition(Position position)
        {
            int index = GameComponentsLookup.Position;
            var component = CreateComponent<PositionComponent>(index);
            component.Value = position;
            ReplaceComponent(index, component);
        }

    }

}
