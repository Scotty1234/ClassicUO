using Microsoft.Xna.Framework;

using ClassicUO.Game.ECS.Components;
using ClassicUO.Renderer;

namespace ClassicUO.Game.ECS
{
    class GameEntityFactory
    {
        public static GameEntity CreateChunkEntity(ushort x, ushort y)
        {
            GameEntity chunkEntity = Contexts.SharedInstance.Game.CreateEntity();
            chunkEntity.AddChunk();
            chunkEntity.AddPosition2D(x, y);

            int[,] TileEntities = new int[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    TileEntities[i, j] = CreateTileEntity((ushort)(x + i), (ushort)(y + j)).Id.Value;

                }
            }

            chunkEntity.AddTiles(TileEntities);
            chunkEntity.AddLastAccessTime(Engine.Ticks + Constants.CLEAR_TEXTURES_DELAY);

            return chunkEntity;
        }

        public static GameEntity CreateLandEntity(Graphic graphic, sbyte height)
        {
            GameEntity landEntity = CreateBaseGameObjectEntity();

            landEntity.AddHeight(height, height);
            landEntity.AddLand();
            landEntity.AddNormals(new Vector3[4]);
            landEntity.AddStretched();
            landEntity.AddTileData(null);
            landEntity.AddVertices(new SpriteVertex[4]);

            landEntity.ReplaceGraphic(graphic);

            if (graphic <= 2)
            {
                landEntity.RemoveComponent(GameComponentsLookup.Draw);
            }

            landEntity.ReplaceAlphaHue(255);

            return landEntity;
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
            GameEntity staticEntity = CreateBaseGameObjectEntity();

            staticEntity.ReplaceGraphic(graphic);
            staticEntity.ReplaceHue(hue);

            return staticEntity;
        }

        public static GameEntity CreateTileEntity(ushort x, ushort y)
        {
            GameEntity TileEntity = Contexts.SharedInstance.Game.CreateEntity();

            TileEntity.AddPosition2D(x, y);
            TileEntity.AddTile();
           
            return TileEntity;
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
