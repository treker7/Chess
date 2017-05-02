using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Pawn : Piece
    {
        public static readonly double VALUE = 1.0;

        public Pawn(bool white, Square position) : base(white, position)
        { }

        public override double GetValue()
        {
            return Pawn.VALUE;
        }

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (this.White ? "P" : "p");
        }
    }
}
