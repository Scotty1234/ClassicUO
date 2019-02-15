using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class HueComponent : IComponent
    {
        public Hue Hue;
    }

    internal partial class GameEntity
    {
        public void AddHue(Hue hue)
        {
            var component = CreateComponent<HueComponent>(GameComponentsLookup.Hue);
            component.Hue = hue;
            AddComponent(GameComponentsLookup.Hue, component);
        }

        public void ReplaceHue(Hue hue)
        {
            var component = CreateComponent<HueComponent>(GameComponentsLookup.Hue);
            component.Hue = hue;
            ReplaceComponent(GameComponentsLookup.Hue, component);
        }
    }
}
