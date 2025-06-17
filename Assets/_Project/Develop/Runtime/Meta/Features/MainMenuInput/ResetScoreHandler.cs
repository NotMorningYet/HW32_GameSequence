using Assets._Project.Develop.Runtime.Meta.Features.ScoreCount;
using Assets._Project.Develop.Runtime.Meta.ShopFeatures;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

public class ResetScoreHandler : IUpdatable
{
    private Shop _shop;
    private ScoreCounter _scoreCounter;

    private readonly string _resetItemName = "ResetScore";
    private KeyCode _resetKeyCode = KeyCode.R;

    public ResetScoreHandler(Shop shop, ScoreCounter scoreCounter, NonMonoBehUpdater updater)
    {
        _shop = shop;
        _scoreCounter = scoreCounter;
        updater.Add(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(_resetKeyCode))
            TryToResetScore();
    }

    private void TryToResetScore()
    {
        if (_shop.Buy(_resetItemName))
            _scoreCounter.ResetCount();
    }
}
