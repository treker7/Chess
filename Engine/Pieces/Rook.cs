using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Rook : Piece
    {
        public static readonly double VALUE = 5.0;

        public Rook(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Rook.VALUE;
        }

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            char rank = this.Position.Rank;
            char file = this.Position.File;

            int[,] transVec = { {1, 0}, {-1, 0 }, {0, 1}, {0, -1} };
            for(int i = 0; i < transVec.Length; i++)
            {
                int t = 1;
                bool blocked = false;
                char testRank = (char)(rank + (transVec[i, 0] * t));
                char testFile = (char)(rank + (transVec[i, 1] * t));
                while(Square.IsInRange(testRank, testFile) && !blocked)
                {
                    Piece other = board.GetPiece(testRank, testFile);
                    if(other == null)
                    {
                        attacks.Add(new Square(testRank, testFile));
                    }
                    else if(this.White != other.White)
                    {
                        attacks.Add(new Square(testRank, testFile));
                        blocked = true;
                    }
                    else
                    {
                        blocked = true;
                    }
                    t++;
                }
            }
            return attacks;
        }

        public override object Clone()
        {
            return new Rook(this.White, this.Position);
        }

        public override string ToString()
        {
            return (this.White ? "R" : "r");
        }
    }
}
