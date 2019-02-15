using System.Collections.Generic;

using Entitas;

using Microsoft.Xna.Framework;

using ClassicUO.IO;
using ClassicUO.Game.Map;

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

            if (worldEntity.HasMap)
            {
                PositionComponent position = playerEntity.Position;

                worldEntity.ReplaceCenter(new Point(position.X, position.Y));
                Initialise(worldEntity);

                // AddToTile
                // Clear Steps
                // Process Delta
            }
            else
            {
                worldEntity.AddMap();
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
                   // ref Chunk chunk = ref mapComponent.Chunks[cellindex];

                   //// Contexts.SharedInstance.Game.Get

                   // if (chunk == null)
                   // {
                   //     if (Engine.Ticks - tick >= maxDelay)
                   //         return;
                   //     mapComponent.UsedIndices.Add(cellindex);
                   //     chunk = new Chunk((ushort)i, (ushort)j);

                   //     GameEntity chunkEntity = Contexts.SharedInstance.Game.CreateEntity();

                       // chunk.Load(index);
                   // }

                  //  chunk.LastAccessTime = Engine.Ticks;
                }
            }
        }
    }
}
