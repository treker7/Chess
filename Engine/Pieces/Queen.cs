using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Queen : Piece
    {
        public static readonly double VALUE = 9.0;

        public Queen(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Queen.VALUE;
        }

        public override List<Square> GetAttacks(Board board)
        {
            int[,] transVec = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { +1, +1 }, { +1, -1 }, { -1, +1 }, { -1, -1 } };
            return base.GetSliderAttacks(board, transVec);
        }

        public override object Clone()
        {
            return new Queen(this.White, this.Position);
        }

        public override string ToString()
        {
            return (this.White ? "Q" : "q");
        }
    }
}
