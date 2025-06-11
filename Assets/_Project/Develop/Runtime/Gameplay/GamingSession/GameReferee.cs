using System;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameReferee : IDisposable
    {
        public event Action LostGame;
        public event Action WinGame;
                
        private string _sequence;
        private int _length;
        private int _currentindex;
        

        public GameReferee()
        {
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
                LostGame?.Invoke();
                return;
            }
            else
            {
                _currentindex++;
            }

            if (_currentindex == _length)
            {
                WinGame?.Invoke();
            }
        }

        public void Dispose()
        {
            LostGame = null;
            WinGame = null;
        }
    }
}
