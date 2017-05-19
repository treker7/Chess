using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Queen : Piece
    {
        public static readonly int VALUE = 900;

        // the relative values of squares for queens to occupy (from white's perspective)
        private static readonly int[,] positionalMatrix = {
            { -20,-10,-10, -5, -5,-10,-10,-20 },
            { -10,  0,  5,  0,  0,  0,  0,-10 },
            { -10,  5,  5,  5,  5,  5,  0,-10 },
            { 0,  0,  5,  5,  5,  5,  0, -5 },
            { -5,  0,  5,  5,  5,  5,  0, -5 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -20,-10,-10, -5, -5,-10,-10,-20 }
        };

        public Queen(bool white, Square position) : base(white, position)
        { }

        public override int GetValue(Board board)
        {
            int value = Queen.VALUE;

            return value;
        }

        public override Piece MoveTo(Square to)
        {
            return new Queen(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            int[,] transVec = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { +1, +1 }, { +1, -1 }, { -1, +1 }, { -1, -1 } };
            return base.GetSliderAttacks(board, transVec);
        }
        
        public override string ToString()
        {
            return (this.White ? "Q" : "q");
        }
    }
}
