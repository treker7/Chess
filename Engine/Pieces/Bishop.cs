using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Bishop : Piece
    {
        public static readonly double VALUE = 3.0;

        public Bishop(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Bishop.VALUE;
        }

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (this.White ? "B" : "b");
        }
    }
}
