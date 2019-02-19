using System.Collections.Generic;

using Entitas;

using Microsoft.Xna.Framework;

using ClassicUO.IO;
using ClassicUO.Game.Map;
using ClassicUO.IO.Resources;

namespace ClassicUO.Game.ECS
{
    internal sealed class MapIndexSystem : ReactiveSystem<GameEntity>
    {
        public MapIndexSystem(Contexts context) : base(context.Game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {

            GameEntity worldEntity = Contexts.SharedInstance.Game.WorldEntity;

            int mapIndex = worldEntity.MapIndex.Value;

            if (mapIndex < 0 && worldEntity.HasMap)
            {
                worldEntity.RemoveMap();
                return;
            }

            GameEntity playerEntity = Contexts.SharedInstance.Game.PlayerEntity;
            PositionComponent position = playerEntity.Position;

            if (worldEntity.HasMap)
            {

                worldEntity.ReplaceCenter(new Point(position.Value.X, position.Value.Y));
                Initialise(worldEntity);

                // AddToTile
                // Clear Steps
                // Process Delta
            }
            else
            {
                worldEntity.AddMap();

                if (playerEntity != null)
                {
                    worldEntity.ReplaceCenter(new Point(position.Value.X, position.Value.Y));
                }

                Initialise(worldEntity);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.HasComponent(GameComponentsLookup.MapIndex);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MapIndex);
        }

        private void Initialise(GameEntity e)
        {
            const int XY_OFFSET = 30;
            int index = e.Map.Index;

            int minBlockX = ((e.Map.Center.X - XY_OFFSET) >> 3) - 1;
            int minBlockY = ((e.Map.Center.Y - XY_OFFSET) >> 3) - 1;
            int maxBlockX = ((e.Map.Center.X + XY_OFFSET) >> 3) + 1;
            int maxBlockY = ((e.Map.Center.Y + XY_OFFSET) >> 3) + 1;

            if (minBlockX < 0)
            {
                minBlockX = 0;
            }

            if (minBlockY < 0)
            {
	            minBlockY = 0;
            }

            if (maxBlockX >= FileManager.Map.MapBlocksSize[index, 0])
            {
                maxBlockX = FileManager.Map.MapBlocksSize[index, 0] - 1;
            }

            if (maxBlockY >= FileManager.Map.MapBlocksSize[index, 1])
            {
                maxBlockY = FileManager.Map.MapBlocksSize[index, 1] - 1;
            }

            long tick = Engine.Ticks;
            long maxDelay = Engine.FrameDelay[1] >> 1;

            for (int i = minBlockX; i <= maxBlockX; i++)
            {
                int blockIndex = i * FileManager.Map.MapBlocksSize[index, 1];

                for (int j = minBlockY; j <= maxBlockY; j++)
                {
                    int cellindex = blockIndex + j;

                    GameEntity chunkEntity =  Contexts.SharedInstance.Game.GetEntityWithId(e.Chunks[cellindex]);

                    if (chunkEntity == null)
                    {
                        if (Engine.Ticks - tick >= maxDelay)
                        {
                            return;
                        }

                        List<int> usedIndices = e.UsedIndices.UsedIndices;
                        usedIndices.Add(cellindex);
                        e.ReplaceUsedIndices(usedIndices);

                        chunkEntity = GameEntityFactory.CreateChunkEntity((ushort)i, (ushort)j);
                        LoadMap(chunkEntity, e.MapIndex.Value);
                    }

                    chunkEntity.ReplaceLastAccessTime(Engine.Ticks);
                }
            }
        }

        private unsafe void LoadMap(GameEntity chunkEntity, int map)
        {
            //IndexMap im = GetIndex(map);

            //if (im.MapAddress != 0)
            //{


            //    MapBlock* block = (MapBlock*)im.MapAddress;
            //    MapCells* cells = (MapCells*)&block->Cells;
            //    //int bx = chunkEntity.Position2D.X * 8;
            //    //int by = chunkEntity.Position2D.Y * 8;

            //    for (int x = 0; x < 8; x++)
            //    {
            //        for (int y = 0; y < 8; y++)
            //        {
            //            int pos = y * 8 + x;
            //            ushort tileID = (ushort)(cells[pos].TileID & 0x3FFF);
            //            sbyte z = cells[pos].Z;

            //            GameEntity landEntity = GameEntityFactory.CreateLandEntity(tileID, z);

            //            ushort tileX = (ushort)(bx + x);
            //            ushort tileY = (ushort)(by + y);

            //            //land.Calculate(tileX, tileY, z);
            //            //land.Position = new Position(tileX, tileY, z);

            //            //land.AddToTile(Tiles[x, y]);
            //        }
            //    }

            //    if (im.StaticAddress != 0)
            //    {
            //        StaticsBlock* sb = (StaticsBlock*)im.StaticAddress;

            //        if (sb != null)
            //        {
            //            int count = (int)im.StaticCount;

            //            for (int i = 0; i < count; i++, sb++)
            //            {
            //                if (sb->Color != 0 && sb->Color != 0xFFFF)
            //                {
            //                    ushort x = sb->X;
            //                    ushort y = sb->Y;
            //                    int pos = y * 8 + x;

            //                    if (pos >= 64)
            //                        continue;
            //                    sbyte z = sb->Z;

            //                    ushort staticX = (ushort)(bx + x);
            //                    ushort staticY = (ushort)(by + y);

            //                    GameEntity staticEntity = GameEntityFactory.CreateStaticEntity(sb->Color, sb->Hue, pos);
            //                    staticEntity.ReplacePosition(new Position(staticX, staticY, z));

            //                    //if (staticObject.ItemData.IsAnimated)
            //                    //    World.AddEffect(new AnimatedItemEffect(staticObject, staticObject.Graphic, staticObject.Hue, -1));
            //                    //else
            //                    //    staticObject.AddToTile(Tiles[x, y]);
            //                }
            //            }
            //        }
            //    }

            }
    }
}
