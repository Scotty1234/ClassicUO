using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicUO.Game.Systems
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
