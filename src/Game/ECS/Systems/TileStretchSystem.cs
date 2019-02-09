using System.Collections.Generic;
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

                }
            }
        }
    }
}
