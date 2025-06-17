using System;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class GameFinishEventMaker
    {
        public event Action Win;
        public event Action Lost;

        public void TriggerWin() => Win?.Invoke();

        public void TriggerLost() => Lost?.Invoke();
    }
}
