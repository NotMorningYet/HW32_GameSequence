using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class ModeInputHandler : MonoBehaviour
    {
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinePerformer _coroutinePerformer;

        public void Initialize(SceneSwitcherService sceneSwithcer, ICoroutinePerformer coroutinePerformer)
        {
            _coroutinePerformer = coroutinePerformer;
            _sceneSwitcherService = sceneSwithcer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                GoToPlay(ModeType.Digital);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                GoToPlay(ModeType.Literal);
        }

        private void GoToPlay(ModeType type)
        {            
            _coroutinePerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(
                    Scenes.GamePlay,
                    new GameplayInputArgs(type)
                )
            );
        }
    }
}
