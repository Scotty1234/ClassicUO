using ClassicUO.Game.Managers;
using ClassicUO.Game.Systems;
using Entitas;

namespace ClassicUO.Game.ECS.Components
{
    internal sealed class WorldComponent : IComponent
    {
        public int MapIndex;
        public PartyManager Party;
        public PrimaryEntityIndex<GameEntity, int> Player;
        public string ServerName;
        public byte ViewRange;
    }
}
