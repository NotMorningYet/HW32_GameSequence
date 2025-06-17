using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameStarter
    {
        private readonly SequenceGenerator _sequenceGenerator;
        private readonly GameReferee _gameReferee;
        private readonly GameInputHandler _gameInputHandler;
        private ModeType _modeType;

        public GameStarter(
            SequenceGenerator sequenceGenerator,
            GameReferee gameReferee,
            GameInputHandler inputHandler
            )
        {
            _sequenceGenerator = sequenceGenerator;
            _gameReferee = gameReferee;
            _gameInputHandler = inputHandler;
        }

        public void StartGame(ModeType type)
        {
            _modeType = type;
            string _currentSequence = new(_sequenceGenerator.GenerateSequence(_modeType));
            Debug.Log($"Последовательность: {_currentSequence}");
            _gameReferee.SetSequence(_currentSequence);

            _gameInputHandler.StartWorking();
        }
    }
}
