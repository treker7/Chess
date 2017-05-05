using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Engine
    {
        public static Move SearchMoves(Board board, int depth)
        {
            List<Move> moves = board.GetMovesOfSide(board.WhiteMove);

            return moves[0];
        }
    }
}
