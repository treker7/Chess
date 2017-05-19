using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Bishop : Piece
    {
        public static readonly int VALUE = 330;

        // the relative values of squares for bishops to occupy (from white's perspective)
        private static readonly int[,] positionalMatrix = {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  5,  0,  0,  0,  0,  5,-10 },
            { -10, 10, 10, 10, 10, 10, 10,-10 },
            { -10,  0, 10, 10, 10, 10,  0,-10 },
            { -10,  5,  5, 10, 10,  5,  5,-10 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };

        public Bishop(bool white, Square position) : base(white, position)
        { }

        public override int GetValue(Board board)
        {
            int value = Bishop.VALUE;
            // positional consideration          
            if (this.White)
                value += positionalMatrix[this.Position.Rank, this.Position.File];
            else
                value += positionalMatrix[(7 - this.Position.Rank), this.Position.File];

            return value;
        }

        public override Piece MoveTo(Square to)
        {
            return new Bishop(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            int[,] transVec = { { +1, +1 }, { +1, -1 }, { -1, +1 }, { -1, -1 } };
            return base.GetSliderAttacks(board, transVec);
        }
        
        public override string ToString()
        {
            return (this.White ? "B" : "b");
        }
    }
}
