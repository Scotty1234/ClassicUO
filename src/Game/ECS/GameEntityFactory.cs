using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Systems.Components;
using Entitas;

namespace ClassicUO.Game.Systems
{
    class GameEntityFactory
    {
        public static GameEntity CreateChunkEntity(ushort x, ushort y)
        {
            GameEntity e = Contexts.SharedInstance.Game.CreateEntity();

            ChunkComponent chunkComponent = e.CreateComponent<ChunkComponent>(GameComponentsLookup.Chunk);
            //chunkComponent.Tiles = new int[8, 8];

            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        chunkComponent.Tiles[i, j] = CreateTileEntity((ushort)(x + i), (ushort)(y + j)).;

            //    }
            //}

            e.AddComponent(GameComponentsLookup.Chunk, chunkComponent);

            Position2DComponent position2DComponent = e.CreateComponent<Position2DComponent>(GameComponentsLookup.Position2D);
            position2DComponent.X = x;
            position2DComponent.Y = y;
            e.AddComponent(GameComponentsLookup.Position2D, position2DComponent);

            return e;
        }

        public static GameEntity CreateLandEntity(Graphic graphic, sbyte height)
        {
            GameEntity e = CreateBaseGameObjectEntity();

            e.CreateComponent<LandComponent>(GameComponentsLookup.Land);

            e.CreateComponent<NormalsComponent>(GameComponentsLookup.Normals);
            e.CreateComponent<StretchedComponent>(GameComponentsLookup.Stretched);
            e.CreateComponent<TileDataComponent>(GameComponentsLookup.TileData);
            e.CreateComponent<VerticesComponent>(GameComponentsLookup.Vertices);

            GraphicComponent graphicComponent = e.GetComponent(GameComponentsLookup.Graphic) as GraphicComponent;
            graphicComponent.Graphic = graphic;

            if (graphicComponent.Graphic <= 2)
            {
                e.RemoveComponent(GameComponentsLookup.Draw);
            }

            AlphaHueComponent alphaHueComponent = e.GetComponent(GameComponentsLookup.AlphaHue) as AlphaHueComponent;
            alphaHueComponent.AlphaHue = 255;

            HeightComponent heightComponent = e.CreateComponent<HeightComponent>(GameComponentsLookup.Height);
            heightComponent.Average = height;
            heightComponent.Minimum = height;


            return e;
        }

        public static GameEntity CreateMobileEntity()
        {
            GameEntity e = CreateBaseGameObjectEntity();
            return e;
        }

        public static GameEntity CreatePlayerEntity()
        {
            GameEntity e = CreateMobileEntity();
            e.CreateAndAddComponent<PlayerComponent>(GameComponentsLookup.Player);

            return e;
        }

        public static GameEntity CreateStaticEntity(Graphic graphic, Hue hue, int index)
        {
            GameEntity e = CreateBaseGameObjectEntity();

            GraphicComponent graphicComponent = e.GetComponent(GameComponentsLookup.Graphic) as GraphicComponent;
            graphicComponent.Graphic = graphic;

            HueComponent hueComponent = e.GetComponent(GameComponentsLookup.Hue) as HueComponent;
            hueComponent.Hue = hue;

            return e;
        }

        public static GameEntity CreateTileEntity(ushort x, ushort y)
        {
            GameEntity e = Contexts.SharedInstance.Game.CreateEntity();

            Position2DComponent position2DComponent = e.CreateComponent<Position2DComponent>(GameComponentsLookup.Position2D);
            position2DComponent.X = x;
            position2DComponent.Y = y;

            e.AddComponent(GameComponentsLookup.Position2D, position2DComponent);

            return e;
        }

        private static GameEntity CreateBaseGameObjectEntity()
        {
            GameEntity e = Contexts.SharedInstance.Game.CreateEntity();

            e.CreateAndAddComponent<BoundsComponent>(GameComponentsLookup.Bounds);
            e.CreateAndAddComponent<DrawComponent>(GameComponentsLookup.Draw);
            e.CreateAndAddComponent<GraphicComponent>(GameComponentsLookup.Graphic);
            e.CreateAndAddComponent<HueComponent>(GameComponentsLookup.Hue);
            e.CreateAndAddComponent<PositionComponent>(GameComponentsLookup.Position);
            e.CreateAndAddComponent<RealScreenPositionComponent>(GameComponentsLookup.RealScreenPosition);
            e.CreateAndAddComponent<ScreenPositionComponent>(GameComponentsLookup.ScreenPosition);
            e.CreateAndAddComponent<TileComponent>(GameComponentsLookup.Tile);

            return e;
        }


    }
}
