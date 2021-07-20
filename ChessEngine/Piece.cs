using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    interface Piece
    {
        public List<string> getPossibleMoves(Piece[,] board);
    }
}
