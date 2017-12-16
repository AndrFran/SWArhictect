using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWArchitecture
{
    namespace VLPI
    {
        public class Caretaker
        {
            private readonly ProgressSaver _game = new ProgressSaver();
            private readonly Stack<GameMemento> _quickSaves = new Stack<GameMemento>();
            public void BeginTest()
            {
                _game.Go();
            }
            public void F5()
            {
                _quickSaves.Push(_game.GameSave());
            }
            public void F9()
            {
                _game.LoadGame(_quickSaves.Peek());
            }
        }
        public class ProgressSaver
        {
            // Стан містить здоров’я та к-ть вбитих монстрів
            private GameState _state;
            public void Go()
            {
                //Тут процес повинен міститися тестування 
            }
            public GameMemento GameSave()
            {
                return new GameMemento(_state);
            }
            public void LoadGame(GameMemento memento)
            {
                _state = memento.GetState();
            }
        }

        public class GameMemento
        {
            private readonly GameState _state;
            public GameMemento(GameState state)
            {
                _state = state;
            }
            public GameState GetState()
            {
                return _state;
            }
        }

        public class GameState
        {
            private int CountOfPossitiveAnswers { get; }
            private int CountOfNegativeAnswers { get; }
            private int NumberOfCurrentTask { get; }
            private int CurrentTimeLeft { get; }
            private string CurrentAnswer { get; }
            public GameState(int pos, int neg, int num, int tl, string ans)
            {
                CountOfPossitiveAnswers = pos;
                CountOfNegativeAnswers = neg;
                NumberOfCurrentTask = num;
                CurrentTimeLeft = tl;
                CurrentAnswer = ans;
            }
        }
    }


}
