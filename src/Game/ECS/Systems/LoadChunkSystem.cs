using Entitas;
using System;
using System.Collections.Generic;

namespace ClassicUO.Game.ECS
{
    internal sealed class LoadChunkSystem : ReactiveSystem<GameEntity>
    {
        public LoadChunkSystem(Contexts contexts) : base(contexts.Game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            throw new NotImplementedException();
        }

        protected override bool Filter(GameEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            throw new NotImplementedException();
        }
    }
}
