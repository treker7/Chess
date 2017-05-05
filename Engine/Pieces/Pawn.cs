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
                if (Square.IsInRange((rank + 1), (file - 1)) && ((board.GetPiece((rank + 1), (file - 1)) != null) && (this.White != board.GetPiece((rank + 1), (file - 1)).White)) || (((new Square((sbyte)(rank + 1), (sbyte)(file - 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                    attacks.Add(new Square((sbyte)(rank + 1), (sbyte)(file - 1)));
                if (Square.IsInRange((rank + 1), (file + 1)) && ((board.GetPiece((rank + 1), (file + 1)) != null) && (this.White != board.GetPiece((rank + 1), (file + 1)).White)) || (((new Square((sbyte)(rank + 1), (sbyte)(file + 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                    attacks.Add(new Square((sbyte)(rank + 1), (sbyte)(file + 1)));                
            }
            else
            {
                if (Square.IsInRange((rank - 1), (file - 1)) && ((board.GetPiece((rank - 1), (file - 1)) != null) && (this.White != board.GetPiece((rank - 1), (file - 1)).White)) || (((new Square((sbyte)(rank - 1), (sbyte)(file - 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                    attacks.Add(new Square((sbyte)(rank - 1), (sbyte)(file - 1)));
                if (Square.IsInRange((rank - 1), (file + 1)) && ((board.GetPiece((rank - 1), (file + 1)) != null) && (this.White != board.GetPiece((rank - 1), (file + 1)).White)) || (((new Square((sbyte)(rank - 1), (sbyte)(file + 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                    attacks.Add(new Square((sbyte)(rank - 1), (sbyte)(file + 1)));
            }
            return attacks;
        }

        public override List<Move> GetMoves(Board board)
        {
            List<Move> potentialMoves = base.GetMoves(board);

            sbyte rank = this.Position.Rank;
            sbyte file = this.Position.File;
            if (this.White)
            {
                if (Square.IsInRange(rank + 1, file) && (board.GetPiece(rank + 1, file) == null))
                {
                    potentialMoves.Add(new Move(this.Position, new Square((sbyte)(rank + 1), file)));
                    if((rank == 1) && (board.GetPiece(rank + 2, file) == null)) // pawn can move ahead two ranks
                         potentialMoves.Add(new Move(this.Position, new Square((sbyte)(rank + 2), file)));
                }
            }
            else
            {
                if (Square.IsInRange(rank - 1, file) && (board.GetPiece(rank - 1, file) == null))
                {
                    potentialMoves.Add(new Move(this.Position, new Square((sbyte)(rank - 1), file)));
                    if ((rank == 6) && (board.GetPiece(rank - 2, file) == null)) // pawn can move ahead two ranks
                        potentialMoves.Add(new Move(this.Position, new Square((sbyte)(rank - 2), file)));
                }
            }

            List<Move> moves = new List<Move>();
            foreach (Move potentialMove in potentialMoves)
            {                
                if (!board.Move(potentialMove).IsInCheck(this.White))
                {
                    moves.Add(potentialMove);
                }
            }
            return moves;
        }

        public override string ToString()
        {
            return (this.White ? "P" : "p");
        }
    }
}
