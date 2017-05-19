using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Rook : Piece
    {
        public static readonly int VALUE = 500;

        // the relative values of squares for rooks to occupy (from white's perspective)
        private static readonly int[,] positionalMatrix = {
            { 0,  0,  0,  5,  5,  0,  0,  0 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { 5, 10, 10, 10, 10, 10, 10,  5 },
            { 0,  0,  0,  0,  0,  0,  0,  0 }
        };

        public Rook(bool white, Square position) : base(white, position)
        { }

        public override int GetValue(Board board)
        {
            int value = Rook.VALUE;
            // positional consideration          
            if (this.White)
                value += positionalMatrix[this.Position.Rank, this.Position.File];
            else
                value += positionalMatrix[(7 - this.Position.Rank), this.Position.File];

            return value;
        }

        public override Piece MoveTo(Square to)
        {
            return new Rook(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            int[,] transVec = { {1, 0}, {-1, 0 }, {0, 1}, {0, -1} };
            return base.GetSliderAttacks(board, transVec);
        }
        
        public override string ToString()
        {
            return (this.White ? "R" : "r");
        }
    }
}
