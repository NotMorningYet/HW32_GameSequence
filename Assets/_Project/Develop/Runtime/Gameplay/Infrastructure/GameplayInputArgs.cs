using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(ModeType modeType)
        {
            ModeType = modeType;
        }

        public ModeType ModeType { get; }
    }

}
