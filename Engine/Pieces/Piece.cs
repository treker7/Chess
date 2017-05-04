using System;
using System.Collections.Generic;

namespace Engine
{
    // IMMUTABLE CLASS
    internal abstract class Piece
    {
        public Square Position { get; }
        public bool White { get; }        

        public Piece(bool white, Square position)
        {
            this.White = white;
            this.Position = position;
        }

        public abstract double GetValue();
        public abstract Piece MoveTo(Square to);
        public abstract List<Square> GetAttacks(Board board);
        public abstract override string ToString();

        // returns the attacks of a sliding piece (i.e. a rook, bishop, or queen) given movement direction vectors
        public List<Square> GetSliderAttacks(Board board, int[,] transVec)
        {
            List<Square> attacks = new List<Square>();

            for (int i = 0; i < transVec.GetLength(0); i++)
            {
                int t = 1;
                bool blocked = false;
                sbyte testRank = (sbyte)(this.Position.Rank + (transVec[i, 0] * t));
                sbyte testFile = (sbyte)(this.Position.File + (transVec[i, 1] * t));
                while (Square.IsInRange(testRank, testFile) && !blocked)
                {                    
                    Piece other = board.GetPiece(testRank, testFile);
                    if (other == null)
                    {
                        attacks.Add(new Square(testRank, testFile));
                    }
                    else if (this.White != other.White)
                    {
                        attacks.Add(new Square(testRank, testFile));
                        blocked = true;
                    }
                    else
                    {
                        blocked = true;
                    }
                    t++;
                    testRank = (sbyte)(this.Position.Rank + (transVec[i, 0] * t));
                    testFile = (sbyte)(this.Position.File + (transVec[i, 1] * t));
                }
            }
            return attacks;
        }
    }
}