using Assets._Project.Develop.Runtime.Meta.Features.ScoreCount;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput
{
    public class ButtonViewScoreCounterHandler : IUpdatable
    {        
        private ScoreCounterViewer _scoreViewer;

        private KeyCode _viewResultKeyCode = KeyCode.V;

        public ButtonViewScoreCounterHandler(NonMonoBehUpdater updater, ScoreCounterViewer viwer)
        {            
            _scoreViewer = viwer;
            updater.Add(this);
        }

        public void Update()
        {
            if (Input.GetKeyDown(_viewResultKeyCode))
                ShowResults();
        }

        private void ShowResults()
        {
            _scoreViewer.Show();
        }
    }
}
