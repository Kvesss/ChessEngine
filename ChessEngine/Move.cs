using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class Move
    {
        private string move;
        private int value;


        public Move(string move, int value)
        {
            this.move = move;
            this.value = value;
        }

        public string GetMove()
        {
            return move;
        }

        public int GetValue()
        {
            return value;
        }
    }
}
