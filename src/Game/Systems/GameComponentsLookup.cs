using System;

using ClassicUO.Game.Systems.Components;

namespace ClassicUO.Game.Systems
{
    internal static class GameComponentsLookup
    {
        public const int AlphaHue = 2;
        public const int Bounds = 3;
        public const int Draw = -1;
        public const int DrawTransparent = 4;
        public const int Graphic = -1;
        public const int Hue = 1;
        public const int Position = 0;
        public const int RealScreenPosition = -1;
        public const int ScreenPosition = -1;
        public const int Shadow = 0;
        public const int Stretched = -1;
        public const int Texture = -1;
        public const int Tile = -1;
        public const int TileData = -1;

        public const int TotalComponents = 10;

        public static readonly string[] ComponentNames =
        {
            "AlphaHue",
            "Bounds",
            "DrawTransparent",
            "Graphic",
            "Hue",
            "Position",
            "Shadow",
            "Texture",
            "Tile",
        };

        public static readonly Type[] ComponentTypes =
        {
            typeof(AlphaHueComponent),
            typeof(DrawTransparentComponent),
            typeof(GraphicComponent),
            typeof(HueComponent),
            typeof(PositionComponent),
        };
    }
}
