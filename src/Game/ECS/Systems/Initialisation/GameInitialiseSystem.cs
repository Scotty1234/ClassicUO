using ClassicUO.Game.ECS.Components;
using ClassicUO.Game.Systems;
using ClassicUO.Game.Systems.Components;
using ClassicUO.IO;
using Entitas;

namespace ClassicUO.Game.ECS.Systems.Initialisation
{
    class GameInitialiseSystem : IInitializeSystem
    {
        public void Initialize()
        {
            //  Create map entity.
            GameEntity e = Contexts.SharedInstance.Game.CreateEntity();

            MapComponent mapComponent = e.CreateComponent<MapComponent>(GameComponentsLookup.Map);
            //e.add
            //FileManager.Map
        }

    }
}
