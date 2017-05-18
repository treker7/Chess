using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Bishop : Piece
    {
        public static readonly float VALUE = 3.0F;

        private static readonly int MAX_NUM_ATTACKS = 13; // the maximum number of squares a bishop can attack at once

        public Bishop(bool white, Square position) : base(white, position)
        { }

        public override float GetValue(Board board)
        {
            float value = Bishop.VALUE;

            // positional considerations
            // bishops are more powerful when they can attack more squares
            value += (this.GetAttacks(board).Count / (float)MAX_NUM_ATTACKS) * Piece.MOBILITY_FACTOR;
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
