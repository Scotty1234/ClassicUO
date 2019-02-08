using ClassicUO.Game.Systems.Components;

namespace ClassicUO.Game.Systems
{
    class GameEntityFactory
    {
        public static GameEntity CreateLandEntity(Graphic graphic)
        {
            GameEntity e = CreateBaseGameObjectEntity();

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

            return e;
        }

        public static GameEntity CreateMobileEntity()
        {
            GameEntity e = CreateBaseGameObjectEntity();
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

        private static GameEntity CreateBaseGameObjectEntity()
        {
            GameEntity e = Contexts.SharedInstance.Game.CreateEntity();

            e.CreateComponent<BoundsComponent>(GameComponentsLookup.Bounds);
            e.CreateComponent<DrawComponent>(GameComponentsLookup.Draw);
            e.CreateComponent<GraphicComponent>(GameComponentsLookup.Graphic);
            e.CreateComponent<HueComponent>(GameComponentsLookup.Hue);
            e.CreateComponent<PositionComponent>(GameComponentsLookup.Position);
            e.CreateComponent<RealScreenPositionComponent>(GameComponentsLookup.RealScreenPosition);
            e.CreateComponent<ScreenPositionComponent>(GameComponentsLookup.ScreenPosition);
            e.CreateComponent<TileComponent>(GameComponentsLookup.Tile);

            return e;
        }
    }
}
