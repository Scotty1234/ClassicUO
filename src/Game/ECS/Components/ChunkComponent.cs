using ClassicUO.Game.Systems;
using Entitas;

namespace ClassicUO.Game.ECS.Components
{
    internal sealed class ChunkComponent : IComponent
    {
        public int[,] Tiles;
    }
}
