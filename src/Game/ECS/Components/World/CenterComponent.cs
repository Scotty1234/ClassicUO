using Entitas;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.ECS
{
    internal sealed class CenterComponent : IComponent
    {
        public Point Center;
    }

    internal partial class GameEntity
    {

        public void ReplaceCenter(Point newCenter)
        {
            var component = CreateComponent<CenterComponent>(GameComponentsLookup.Center);
            component.Center = newCenter;
            ReplaceComponent(GameComponentsLookup.Center, component);
        }
    }
}
