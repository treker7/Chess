using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Queen : Piece
    {
        public static readonly float VALUE = 9.0F;

        public Queen(bool white, Square position) : base(white, position)
        { }

        public override float GetValue()
        {
            return Queen.VALUE;
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
