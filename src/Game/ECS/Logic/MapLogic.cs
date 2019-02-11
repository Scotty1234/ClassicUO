using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Map;
using ClassicUO.Game.Systems;
using ClassicUO.IO;

namespace ClassicUO.Game.ECS.Logic
{
    //internal static class MapLogic
    //{
    //    public static Tile GetTile(MapComponent mapComponent, short x, short y, bool load = true)
    //    {
    //        if (x < 0 || y < 0)
    //        {
    //            return null;
    //        }

    //        int cellX = x >> 3;
    //        int cellY = y >> 3;
    //        int block = GetBlock(mapComponent.Index, cellX, cellY);

    //        if (block >= mapComponent.Chunks.Length)
    //            return null;
    //        ref Chunk chuck = ref Chunks[block];

    //        if (chuck == null)
    //        {
    //            if (load)
    //            {
    //                _usedIndices.Add(block);
    //                chuck = new Chunk((ushort)cellX, (ushort)cellY);
    //                chuck.Load(Index);
    //            }
    //            else
    //                return null;
    //        }

    //        chuck.LastAccessTime = Engine.Ticks;
    //        return chuck.Tiles[x % 8, y % 8];
    //    }

    //    public static Tile GetTile(int x, int y, bool load = true)
    //    {
    //        return GetTile((short)x, (short)y, load);
    //    }

    //    private static int GetBlock(int index, int blockX, int blockY)
    //    {
    //        return blockX * FileManager.Map.MapBlocksSize[index, 1] + blockY;
    //    }
    //}
}
