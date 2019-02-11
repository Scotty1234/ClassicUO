using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Systems.Components;
using ClassicUO.IO;
using Entitas;

namespace ClassicUO.Game.Systems
{
    internal class TileStretchSystem : ReactiveSystem<GameEntity>
    {
        public TileStretchSystem(Contexts contexts) : base(contexts.Game)
        {

        }

        protected sealed override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Land);
        }

        protected sealed override bool Filter(GameEntity entity)
        {
            return entity.HasComponent(GameComponentsLookup.Land);
        }

        protected sealed override void Execute(List<GameEntity> entities)
        {
            Map.Map map = World.Map;

            for (int i = 0; i < entities.Count; i++)
            {
                TileDataComponent tileDataComponent = entities[i].GetComponent(GameComponentsLookup.TileData) as TileDataComponent;
                GraphicComponent graphicComponent = entities[i].GetComponent(GameComponentsLookup.Graphic) as GraphicComponent;

                if(!tileDataComponent.Value.HasValue)
                {
                    tileDataComponent.Value = FileManager.TileData.LandData[graphicComponent.Graphic];
                }

                bool textureInvalid = FileManager.Textmaps.GetTexture(tileDataComponent.Value.Value.TexID) == null;

                if (entities[i].HasComponent(GameComponentsLookup.Stretched) || textureInvalid)
                {
                    entities[i].RemoveComponent(GameComponentsLookup.Stretched);
                }
                else
                {
                    entities[i].CreateComponent<StretchedComponent>(GameComponentsLookup.Stretched);
                   // UpdateZ(entities[i], map.GetTileZ(x, y + 1), map.GetTileZ(x + 1, y + 1), map.GetTileZ(x + 1, y), z);

                }
            }
        }

        private void UpdateZ(GameEntity e, int zTop, int zRight, int zBottom, sbyte currentZ)
        {
            Debug.Assert(e.HasComponent(GameComponentsLookup.Stretched));

            int x = currentZ * 4 + 1;
            int y = zTop * 4;
            int w = zRight * 4 - x;
            int h = zBottom * 4 + 1 - y;

            HeightComponent     heightComponent = e.GetComponent(GameComponentsLookup.Height) as HeightComponent;
            RectangleComponent  rectangleComponent = e.GetComponent(GameComponentsLookup.Rectangle) as RectangleComponent;

            rectangleComponent.Rectangle.X = x;
            rectangleComponent.Rectangle.Y = y;
            rectangleComponent.Rectangle.Width = w;
            rectangleComponent.Rectangle.Height = h;

            if (Math.Abs(currentZ - zRight) <= Math.Abs(zBottom - zTop))
            {
                heightComponent.Average = (sbyte)((currentZ + zRight) >> 1);
            }
            else
            {
                heightComponent.Average = (sbyte)((zBottom + zTop) >> 1);
            }

            heightComponent.Minimum = currentZ;

            if (zTop < heightComponent.Minimum)
            {
                heightComponent.Minimum = (sbyte)zTop;
            }

            if (zRight < heightComponent.Minimum)
            {
                heightComponent.Minimum = (sbyte)zRight;
            }

            if (zBottom < heightComponent.Minimum)
            {
                heightComponent.Minimum = (sbyte)zBottom;
            }
        }
    }
}
