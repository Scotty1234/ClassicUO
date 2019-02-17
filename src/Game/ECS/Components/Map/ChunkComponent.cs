using ClassicUO.Game.ECS;
using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class ChunkComponent : IComponent
    {
        public int[,] Tiles;
    }
}
