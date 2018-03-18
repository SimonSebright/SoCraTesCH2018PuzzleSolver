using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var startState = GetStartState();
        }

        private static State GetStartState()
        {
            return new State();
        }

        public State RecSearch(State state)
        {
            foreach (var act in state.next_actions())
            {
                var new_state = state.apply(act);

                if (new_state.solved())
                {
                    return new_state;
                }
                else if (new_state.consistent())
                {
                    var result_state = RecSearch(new_state);

                    if (result_state != null && result_state.solved())
                    {
                        return result_state;
                    }
                }
            }

            return null;
        }
    }

    public class State
    {
        private readonly int[,] _board = new int[4, 4];

        public State()
        {
        }

        public State(State basis, int x, int y, int value)
        {
            CopyBoard(basis);

            _board[x, y] = value;
        }

        private void CopyBoard(State basis)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    _board[i, j] = basis._board[i, j];
                }
            }
        }

        internal IEnumerable<GameAction> next_actions()
        {
            throw new NotImplementedException();
        }

        public State apply(GameAction act)
        {
            return new State(this, act._x, act._y, act._value);
        }

        public bool solved()
        {
            return false;
        }

        public bool consistent()
        {
            return false;
        }
    }

    public class GameAction
    {
        public int _x, _y, _value;
        public GameAction(int x, int y, int value)
        {
            _x = x;
            _y = y;
            _value = value;

        }
    }