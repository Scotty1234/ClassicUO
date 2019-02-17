using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class ChunksComponent : IComponent
    {
        public int[] Chunks;
    }

    internal partial class GameEntity
    {
        public int[] Chunks
        {
            get
            {
                var component = (GetComponent(GameComponentsLookup.Chunks) as ChunksComponent);
                return component.Chunks;
            }
        }
    }
}
