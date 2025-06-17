using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreCount
{
    public class ScoreCounterViewer : IDisposable
    {
        private ScoreCounter _scoreCounter;

        public ScoreCounterViewer(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.ValueChanged += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            Show();
        }

        public void Show()
        {
            Debug.Log($"Выигрышей: {_scoreCounter.WinCount}, проигрышей: {_scoreCounter.LostCount}");
        }

        public void Dispose()
        {
            _scoreCounter.ValueChanged -= OnScoreChanged;
        }
    }
}
