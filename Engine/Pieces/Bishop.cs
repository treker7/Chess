using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Bishop : Piece
    {
        public static readonly float VALUE = 3.0F;

        public Bishop(bool white, Square position) : base(white, position)
        { }

        public override float GetValue()
        {
            return Bishop.VALUE;
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
