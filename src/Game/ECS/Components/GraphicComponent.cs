using ClassicUO.Game.ECS;
using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class GraphicComponent : IComponent
    {
        public ushort Graphic;
    }

    internal partial class GameEntity
    {
        public void AddGraphic(ushort graphic)
        {
            var component = CreateComponent<GraphicComponent>(GameComponentsLookup.Graphic);
            component.Graphic = graphic;
            AddComponent(GameComponentsLookup.Graphic, component);
        }

        public void ReplaceGraphic(ushort graphic)
        {
            var component = CreateComponent<GraphicComponent>(GameComponentsLookup.Graphic);
            component.Graphic = graphic;
            ReplaceComponent(GameComponentsLookup.Graphic, component);
        }
    }
}
