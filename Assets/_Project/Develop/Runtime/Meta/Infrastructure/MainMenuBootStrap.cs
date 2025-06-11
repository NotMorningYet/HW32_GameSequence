using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootStrap : SceneBootStrap
    {
        private DIContainer _container;
        private ModeInputHandler _inputHandler;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _inputHandler = _container.Resolve<ModeInputHandler>();
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Press 1 to begin Digital sequence Game");
            Debug.Log("Press 2 to begin Literal sequence Game");
        }
    }
}
