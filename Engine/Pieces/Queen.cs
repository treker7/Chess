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

        public override List<Square> GetMoves()
        {
            throw new NotImplementedException();
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
