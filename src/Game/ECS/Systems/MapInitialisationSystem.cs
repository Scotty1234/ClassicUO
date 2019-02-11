using System.Collections.Generic;
using System.Diagnostics;

using Entitas;

using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Systems;
using ClassicUO.IO;
using ClassicUO.Game.Map;

namespace ClassicUO.Game.ECS.Systems
{
    internal sealed class MapInitialisationSystem : ReactiveSystem<GameEntity>
    {
        public MapInitialisationSystem(Contexts context) : base(context.Game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            Debug.Assert(entities.Count == 1);

            GameEntity e = entities[0];

            MapComponent mapComponent = e.GetComponent(GameComponentsLookup.Map) as MapComponent;

            InitialiseMap(e, mapComponent);

        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.HasComponent(GameComponentsLookup.Map);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Map);
        }

        private void InitialiseMap(GameEntity WorldEntity, MapComponent mapComponent)
        {

            WorldEntity.RemoveComponent(GameComponentsLookup.Map);

            if (mapComponent.Index < 0)
            {
                return;
            }

            Initialise(mapComponent);

        }

        private void Initialise(MapComponent mapComponent)
        {
            const int XY_OFFSET = 30;
            int index = mapComponent.Index;

            int minBlockX = ((mapComponent.Center.X - XY_OFFSET) >> 3) - 1;
            int minBlockY = ((mapComponent.Center.Y - XY_OFFSET) >> 3) - 1;
            int maxBlockX = ((mapComponent.Center.X + XY_OFFSET) >> 3) + 1;
            int maxBlockY = ((mapComponent.Center.Y + XY_OFFSET) >> 3) + 1;

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
                    ref Chunk chunk = ref mapComponent.Chunks[cellindex];

                    if (chunk == null)
                    {
                        if (Engine.Ticks - tick >= maxDelay)
                            return;
                        mapComponent.UsedIndices.Add(cellindex);
                        chunk = new Chunk((ushort)i, (ushort)j);
                        chunk.Load(index);
                    }

                    chunk.LastAccessTime = Engine.Ticks;
                }
            }
        }
    }
}
