using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Rook : Piece
    {
        public static readonly double VALUE = 5.0;

        public Rook(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Rook.VALUE;
        }

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            return new Rook(this.White, this.Position);
        }

        public override string ToString()
        {
            return (this.White ? "R" : "r");
        }
    }
}
