using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput
{
    public class ButtonSaveLoadHandler : IUpdatable
    {
        private ScoreDataProvider _scoreDataProvider;
        private ICoroutinePerformer _coroutinePerformer;

        private KeyCode _saveKeyCode = KeyCode.S;
        private KeyCode _loadKeyCode = KeyCode.L;

        public ButtonSaveLoadHandler(ICoroutinePerformer coroutinePerformer, ScoreDataProvider scoreDataProvider, NonMonoBehUpdater updater)
        {   
            _scoreDataProvider = scoreDataProvider;
            _coroutinePerformer = coroutinePerformer;
            updater.Add(this);
        }

        public void Update()
        {
            if (Input.GetKeyUp(_saveKeyCode))
                Save();

            if (Input.GetKeyUp(_loadKeyCode))   
                Load();
        }

        private void Load()
        {
            _coroutinePerformer.StartPerform(_scoreDataProvider.Load());
            Debug.Log("Данные загружены");
        }

        private void Save()
        { 
            _coroutinePerformer.StartPerform(_scoreDataProvider.Save());
            Debug.Log("Данные сохранены");
        }
    }
}
