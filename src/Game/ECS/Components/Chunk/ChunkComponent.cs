using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class ChunkComponent : IComponent
    {
    }

    internal partial class GameEntity
    {
        public void AddChunk()
        {
            var component = CreateComponent<ChunkComponent>(GameComponentsLookup.Chunk);
            AddComponent(GameComponentsLookup.Chunk, component);
        }
    }
}
