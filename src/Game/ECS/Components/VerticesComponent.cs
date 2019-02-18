using ClassicUO.Renderer;
using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class VerticesComponent : IComponent
    {
        public SpriteVertex[] Vertices;
    }

    internal partial class GameEntity
    {
        public void AddVertices(SpriteVertex[] vertices)
        {
            int index = GameComponentsLookup.Vertices;
            var component = CreateComponent<VerticesComponent>(index);
            component.Vertices = vertices;
            AddComponent(index, component);
        }
    }
}
