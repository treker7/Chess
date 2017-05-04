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

        public override Piece MoveTo(Square to)
        {
            return new Pawn(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            sbyte rank = this.Position.Rank;
            sbyte file = this.Position.File;
            if (White)
            {
                if (Square.IsInRange((sbyte)(rank + 1), (sbyte)(file - 1)) && (board.GetPiece((sbyte)(rank + 1), (sbyte)(file - 1)) != null) && (this.White != board.GetPiece((sbyte)(rank + 1), (sbyte)(file - 1)).White))
                    attacks.Add(new Square((sbyte)(rank + 1), (sbyte)(file - 1)));
                if (Square.IsInRange((sbyte)(rank + 1), (sbyte)(file + 1)) && (board.GetPiece((sbyte)(rank + 1), (sbyte)(file + 1)) != null) && (this.White != board.GetPiece((sbyte)(rank + 1), (sbyte)(file + 1)).White))
                    attacks.Add(new Square((sbyte)(rank + 1), (sbyte)(file + 1)));
            }
            else
            {
                if (Square.IsInRange((sbyte)(rank - 1), (sbyte)(file - 1)) && (board.GetPiece((sbyte)(rank - 1), (sbyte)(file - 1)) != null) && (this.White != board.GetPiece((sbyte)(rank - 1), (sbyte)(file - 1)).White))
                    attacks.Add(new Square((sbyte)(rank - 1), (sbyte)(file - 1)));
                if (Square.IsInRange((sbyte)(rank - 1), (sbyte)(file + 1)) && (board.GetPiece((sbyte)(rank - 1), (sbyte)(file + 1)) != null) && (this.White != board.GetPiece((sbyte)(rank - 1), (sbyte)(file + 1)).White))
                    attacks.Add(new Square((sbyte)(rank - 1), (sbyte)(file + 1)));
            }
            return attacks;
        }

        public override string ToString()
        {
            return (this.White ? "P" : "p");
        }
    }
}
