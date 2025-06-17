using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.ScoreCount
{
    [CreateAssetMenu(menuName = "Configs/ScoreCounter/NewScoreCounterConfig", fileName = "ScoreCounterConfig")]

    public class StartScoreCounterConfig : ScriptableObject
    {
        [SerializeField] private int _winCount;
        [SerializeField] private int _lostCount;

        public int WinCount => _winCount;
        public int LostCount => _lostCount;
    }
}
