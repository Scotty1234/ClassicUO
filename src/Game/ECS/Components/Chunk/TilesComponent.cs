using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class TilesComponent : IComponent
    {
        public int[,] Tiles;
    }

    internal partial class GameEntity
    {
        public void AddTiles(int[,] tiles)
        {
            int index = GameComponentsLookup.Tiles;
            var component = CreateComponent<TilesComponent>(index);
            component.Tiles = tiles;
            AddComponent(index, component);
        }
    }
}
