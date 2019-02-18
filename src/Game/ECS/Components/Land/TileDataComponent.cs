using ClassicUO.IO.Resources;
using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class TileDataComponent : IComponent
    {
        public LandTiles? Value;
    }

    internal partial class GameEntity
    {
        public void AddTileData(LandTiles? landTiles)
        {
            int index = GameComponentsLookup.TileData;
            var component = CreateComponent<TileDataComponent>(index);
            component.Value = landTiles;
            AddComponent(index, component);
        }
    }
}
