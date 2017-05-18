using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Rook : Piece
    {
        public static readonly float VALUE = 5.0F;

        private static readonly int MAX_NUM_ATTACKS = 14; // the maximum number of squares a rook can attack at once

        public Rook(bool white, Square position) : base(white, position)
        { }

        public override float GetValue(Board board)
        {
            float value = Rook.VALUE;

            // positional considerations
            // rooks are more powerful when they can attack more squares
            value += (this.GetAttacks(board).Count / (float)MAX_NUM_ATTACKS) * Piece.MOBILITY_FACTOR;
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
