using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    interface Player
    {
        List<string> GetPossibleMoves();
    }
}
