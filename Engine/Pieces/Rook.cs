using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Rook : Piece
    {
        public static readonly float VALUE = 5.0F;

        public Rook(bool white, Square position) : base(white, position)
        { }

        public override float GetValue()
        {
            return Rook.VALUE;
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
