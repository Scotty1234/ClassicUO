using Entitas;

namespace ClassicUO.Game.ECS
{
    internal sealed class LastAccessTimeComponent : IComponent
    {
        public long LastAccessTime;
    }

    internal partial class GameEntity
    {
        public long LastAccessTime
        {
            get
            {
                var component = (GetComponent(GameComponentsLookup.Chunks) as LastAccessTimeComponent);
                return component.LastAccessTime;
            }
        }

        public void AddLastAccessTime(long time)
        {
            var component = CreateComponent<LastAccessTimeComponent>(GameComponentsLookup.LastAccessTime);
            component.LastAccessTime = time;
            AddComponent(GameComponentsLookup.LastAccessTime, component);
        }

        public void ReplaceLastAccessTime(long time)
        {
            var component = CreateComponent<LastAccessTimeComponent>(GameComponentsLookup.LastAccessTime);
            component.LastAccessTime = time;
            ReplaceComponent(GameComponentsLookup.LastAccessTime, component);
        }
    }

}
