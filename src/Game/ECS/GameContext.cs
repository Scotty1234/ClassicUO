using Entitas;

namespace ClassicUO.Game.Systems
{
    internal sealed partial class GameContext : Context<GameEntity>
    {

        public GameContext(): base(GameComponentsLookup.TotalComponents, 0, new ContextInfo("Game",
                    GameComponentsLookup.ComponentNames,
                    GameComponentsLookup.ComponentTypes
                ),
                (entity) =>

#if (ENTITAS_FAST_AND_UNSAFE)
                new Entitas.UnsafeAERC(),
#else
                new Entitas.SafeAERC(entity),
#endif
            () => new GameEntity()
            )
        {
        }
    }
}
