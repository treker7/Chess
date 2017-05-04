using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Knight : Piece
    {
        public static readonly double VALUE = 3.0;

        public Knight(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Knight.VALUE;
        }

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            char rank = this.Position.Rank;
            char file = this.Position.File;

            int[,] deltas = { { +2, +1 }, { +2, -1 }, { -2, +1 }, { -2, -1 }, { +1, +2 }, { +1, -2 }, { -1, +2 }, { -1, -2 } };

            for(int d = 0; d < deltas.Length; d++)
            {
                char testRank = (char)deltas[d, 0];
                char testFile = (char)deltas[d, 1];
                if (Square.IsInRange(testRank, testFile) && ((board.GetPiece(testRank, testFile) == null) || (this.White != board.GetPiece(testRank, testFile).White)))
                {
                    attacks.Add(new Square(testRank, testFile));
                }
            }
            return attacks;
        }

        public override string ToString()
        {
            return (this.White ? "N" : "n");
        }
    }
}
