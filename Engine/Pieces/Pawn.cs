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

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            char rank = this.Position.Rank;
            char file = this.Position.File;
            if (White)
            {
                if (Square.IsInRange((char)(rank + 1), (char)(file - 1)) && (board.GetPiece((char)(rank + 1), (char)(file - 1)) != null) && (this.White != board.GetPiece((char)(rank + 1), (char)(file - 1)).White))
                    attacks.Add(new Square((char)(rank + 1), (char)(file - 1)));
                if (Square.IsInRange((char)(rank + 1), (char)(file + 1)) && (board.GetPiece((char)(rank + 1), (char)(file + 1)) != null) && (this.White != board.GetPiece((char)(rank + 1), (char)(file + 1)).White))
                    attacks.Add(new Square((char)(rank + 1), (char)(file + 1)));
            }
            else
            {
                if (Square.IsInRange((char)(rank - 1), (char)(file - 1)) && (board.GetPiece((char)(rank - 1), (char)(file - 1)) != null) && (this.White != board.GetPiece((char)(rank - 1), (char)(file - 1)).White))
                    attacks.Add(new Square((char)(rank - 1), (char)(file - 1)));
                if (Square.IsInRange((char)(rank - 1), (char)(file + 1)) && (board.GetPiece((char)(rank - 1), (char)(file + 1)) != null) && (this.White != board.GetPiece((char)(rank - 1), (char)(file + 1)).White))
                    attacks.Add(new Square((char)(rank - 1), (char)(file + 1)));
            }
            return attacks;
        }

        public override string ToString()
        {
            return (this.White ? "P" : "p");
        }
    }
}
