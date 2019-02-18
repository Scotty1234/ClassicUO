using Entitas;

namespace ClassicUO.Game.ECS
{
    internal class AlphaHueComponent : IComponent
    {
        public byte Value;
    }

    internal partial class GameEntity
    {
        public void ReplaceAlphaHue(byte alphaHue)
        {
            int index = GameComponentsLookup.AlphaHue;
            var component = CreateComponent<AlphaHueComponent>(index);
            component.Value = alphaHue;
            AddComponent(index, component);
        }
    }
}
