using Assets._Project.Develop.Runtime.Meta.Infrastructure;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameReferee 
    {
        private GameFinishEventMaker _gameFinishEventMaker;
        private string _sequence;
        private int _length;
        private int _currentindex;
        

        public GameReferee(GameFinishEventMaker eventmaker)
        {
            _gameFinishEventMaker = eventmaker;
            _sequence = string.Empty;
            _length = _sequence.Length;
            _currentindex = 0;
        }

        public void SetSequence(string sequence)
        {
            _sequence = sequence;
            _length = _sequence.Length;
            _currentindex = 0;
        }

        public void CheckInput(char input)
        {
            if (input != _sequence[_currentindex])
            {
                _gameFinishEventMaker.TriggerLost();
                return;
            }
            else
            {
                _currentindex++;
            }

            if (_currentindex == _length)
            {
                _gameFinishEventMaker.TriggerWin();
            }
        }
    }
}
