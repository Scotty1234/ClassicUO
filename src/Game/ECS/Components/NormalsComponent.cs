using Entitas;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.ECS
{
    internal sealed class NormalsComponent : IComponent
    {
        public Vector3[] Normals;
    }

    internal partial class GameEntity
    {
        public void AddNormals(Vector3[] normals)
        {
            int index = GameComponentsLookup.Normals;
            var component = CreateComponent<NormalsComponent>(index);
            component.Normals = normals;
            AddComponent(index, component);
        }
    }
}
