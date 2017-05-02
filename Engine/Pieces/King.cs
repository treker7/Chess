using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class King : Piece
    {
        public static readonly double VALUE = Int16.MaxValue;

        public King(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return King.VALUE;
        }

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (this.White ? "K" : "k");
        }
    }
}
