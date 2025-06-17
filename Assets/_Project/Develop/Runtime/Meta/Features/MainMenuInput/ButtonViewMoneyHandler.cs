using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput
{
    public class ButtonViewMoneyHandler : IUpdatable
    {
        private MoneyViewer _moneyViewer;

        private KeyCode _viewMoneyKeyCode = KeyCode.M;

        public ButtonViewMoneyHandler(NonMonoBehUpdater updater, MoneyViewer viewer)
        {
            _moneyViewer = viewer;
            updater.Add(this);
        }

        public void Update()
        {
            if (Input.GetKeyDown(_viewMoneyKeyCode))
                ShowResults();
        }

        private void ShowResults()
        {
            _moneyViewer.Show();
        }
    }
}
