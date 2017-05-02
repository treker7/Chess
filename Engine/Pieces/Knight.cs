using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Knight : Piece
    {
        public static readonly double VALUE = 3.0;

        public Knight(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Knight.VALUE;
        }

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (this.White ? "N" : "n");
        }
    }
}
