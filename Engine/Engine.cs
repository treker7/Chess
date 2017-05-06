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
            if (moves.Count == 0)
                return null;

            int bestBoardIndex = 0;
            float bestBoardEval = MinMax(Board.Move(board, moves[0]), depth - 1);
            for(int i = 1; i < moves.Count; i++)
            {
                float currBoardEval = MinMax(Board.Move(board, moves[i]), depth - 1);
                if (board.WhiteMove) // maximizing player
                {
                    if (currBoardEval > bestBoardEval)
                    {
                        bestBoardEval = currBoardEval;
                        bestBoardIndex = i;
                    }
                }
                else // minimizing player
                {
                    if (currBoardEval < bestBoardEval)
                    {
                        bestBoardEval = currBoardEval;
                        bestBoardIndex = i;
                    }
                }
            }
            return moves[bestBoardIndex];
        }

        private static float MinMax(Board board, int depth)
        {
            List<Move> moves = board.GetMovesOfSide(board.WhiteMove);
            if ((moves.Count == 0) && board.IsInCheck(board.WhiteMove)) // check mate for this player
            {
                return board.WhiteMove ? (Int16.MinValue - depth): (Int16.MaxValue + depth);
            }
            else if(moves.Count == 0) // stalemate
            {
                return 0;
            }
            else if(depth == 0) // reached maximum depth
            {
                return board.Eval();
            }
            
            float bestBoardEval = MinMax(Board.Move(board, moves[0]), depth - 1);
            for (int i = 1; i < moves.Count; i++)
            {
                float currBoardEval = MinMax(Board.Move(board, moves[i]), depth - 1);
                if (board.WhiteMove) // max
                {
                    if (currBoardEval > bestBoardEval)
                        bestBoardEval = currBoardEval;
                }
                else // min
                {
                    if (currBoardEval < bestBoardEval)
                        bestBoardEval = currBoardEval;
                }
            }
            return bestBoardEval;
        }
    }
}
