﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Engine
    {
        public static readonly int MAX_DEPTH = 4;

        public static Move SearchMoves(Board board, int depth)
        {
            return SearchMovesAlphaBeta(board, depth, Int32.MinValue, Int32.MaxValue);
        }

        private static Move SearchMovesAlphaBeta(Board board, int depth, double alpha, double beta)
        {
            List<Move> moves = board.GetMovesOfSide(board.WhiteMove);
            if (moves.Count == 0)
                return null;

            int bestBoardIndex = 0;
            for (int i = 0; i < moves.Count; i++)
            {
                double currBoardEval;
                if (board.WhiteMove) // maximizing player
                {
                    currBoardEval = AlphaBeta(Board.Move(board, moves[i]), depth - 1, alpha, beta);
                    if (currBoardEval > alpha)
                    {
                        alpha = currBoardEval;
                        bestBoardIndex = i;
                    }
                }
                else // minimizing player
                {
                    currBoardEval = AlphaBeta(Board.Move(board, moves[i]), depth - 1, alpha, beta);
                    if (currBoardEval < beta)
                    {
                        beta = currBoardEval;
                        bestBoardIndex = i;
                    }                    
                }
                if (alpha >= beta) // prune
                    break;
            }
            return moves[bestBoardIndex];
        }

        private static double AlphaBeta(Board board, int depth, double alpha, double beta)
        {
            bool inCheck = board.IsInCheck(board.WhiteMove); // check for this player
            // reached maximum search depth
            if (!inCheck && (depth == 0))
            {
                return board.Eval();
            }
            // not yet reached maximum search depth
            List<Move> moves = board.GetMovesOfSide(board.WhiteMove);
            if ((moves.Count == 0) && inCheck) // check mate for this player
            {
                return board.WhiteMove ? (Int16.MinValue - depth) : (Int16.MaxValue + depth); // greater depth means we found mate sooner which is preferable
            }
            else if(moves.Count == 0) // stalemate
            {
                return board.WhiteMove ? -5: 5; // stalemate is only good if the side to move is currently losing 
            }
            else if(depth == 0)
            {
                return board.Eval();
            }
            
            for (int i = 0; i < moves.Count; i++)
            {
                double currBoardEval;
                if (board.WhiteMove) // max node
                {
                    currBoardEval = AlphaBeta(Board.Move(board, moves[i]), depth - 1, alpha, beta);
                    if (currBoardEval > alpha)
                        alpha = currBoardEval;                    
                }
                else // min node
                {
                    currBoardEval = AlphaBeta(Board.Move(board, moves[i]), depth - 1, alpha, beta);
                    if (currBoardEval < beta)
                        beta = currBoardEval;                    
                }
                if (alpha >= beta) // prune
                    break;
            }
            return board.WhiteMove ? alpha : beta;
        }
    }
}
