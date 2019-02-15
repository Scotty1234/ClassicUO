using Entitas;

namespace ClassicUO.Game.ECS
{
    class GameSystem : IInitializeSystem, IExecuteSystem
    {
        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            //  Create world.
            GameEntity worldEntity = Contexts.SharedInstance.Game.CreateEntity();
            worldEntity.AddWorld();

            //  Create map entity.
            GameEntity mapEntity = Contexts.SharedInstance.Game.CreateEntity();
            mapEntity.AddMap();
            mapEntity.AddMapIndex(-1);

        }

    }
}
