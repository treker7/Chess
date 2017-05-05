using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Knight : Piece
    {
        public static readonly float VALUE = 3.0F;

        public Knight(bool white, Square position) : base(white, position)
        { }

        public override float GetValue()
        {
            return Knight.VALUE;
        }

        public override Piece MoveTo(Square to)
        {
            return new Knight(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            sbyte rank = this.Position.Rank;
            sbyte file = this.Position.File;

            sbyte[,] deltas = { { +2, +1 }, { +2, -1 }, { -2, +1 }, { -2, -1 }, { +1, +2 }, { +1, -2 }, { -1, +2 }, { -1, -2 } };
            for(int d = 0; d < deltas.GetLength(0); d++)
            {
                sbyte testRank = (sbyte)(rank + deltas[d, 0]);
                sbyte testFile = (sbyte)(file + deltas[d, 1]);
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
