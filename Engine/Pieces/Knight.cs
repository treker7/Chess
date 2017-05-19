using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Knight : Piece
    {
        public static readonly int VALUE = 320;

        // the relative values of squares for knights to occupy (from white's perspective)
        private static readonly int[,] positionalMatrix = {
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -30,  5, 10, 15, 15, 10,  5,-30 },
            { -30,  0, 15, 20, 20, 15,  0,-30 },
            { -30,  5, 15, 20, 20, 15,  5,-30 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -40,-20,  0,  0,  0,  0,-20,-40 },
            { -50,-40,-30,-30,-30,-30,-40,-50 }
        };

        public Knight(bool white, Square position) : base(white, position)
        { }

        public override int GetValue(Board board)
        {
            int value = Knight.VALUE;
            // positional consideration          
            if (this.White)
                value += positionalMatrix[this.Position.Rank, this.Position.File];
            else
                value += positionalMatrix[(7 - this.Position.Rank), this.Position.File];

            return value;
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
