using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class TileComponent : IComponent
    {
    }

    internal partial class GameEntity
    {
        public void AddTile()
        {
            var component = CreateComponent<TileComponent>(GameComponentsLookup.Tile);
            AddComponent(GameComponentsLookup.Tile, component);
        }
    }
}
