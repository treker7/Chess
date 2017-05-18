using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Pawn : Piece
    {
        public static readonly float VALUE = 1.0F;

        public Pawn(bool white, Square position) : base(white, position)
        { }

        public override float GetValue()
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

            sbyte checkRank = this.White ? (sbyte)(rank + 1) : (sbyte)(rank - 1);
           
            if (Square.IsInRange(checkRank, (file - 1)) && ((board.GetPiece(checkRank, (file - 1)) != null) && (this.White != board.GetPiece(checkRank, (file - 1)).White)) || (((new Square(checkRank, (sbyte)(file - 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                attacks.Add(new Square(checkRank, (sbyte)(file - 1)));
            if (Square.IsInRange(checkRank, (file + 1)) && ((board.GetPiece(checkRank, (file + 1)) != null) && (this.White != board.GetPiece(checkRank, (file + 1)).White)) || (((new Square(checkRank, (sbyte)(file + 1)).Equals(board.EnPassantSquare)) && (this.White == board.WhiteMove))))
                attacks.Add(new Square(checkRank, (sbyte)(file + 1)));      
            
            return attacks;
        }

        public override List<Move> GetMoves(Board board)
        {
            List<Move> potentialMoves = base.GetMoves(board);
            sbyte rank = this.Position.Rank;
            sbyte file = this.Position.File;

            sbyte checkRank1 = this.White ? (sbyte)(rank + 1) : (sbyte)(rank - 1);
            sbyte checkRank2 = this.White ? (sbyte)(rank + 2) : (sbyte)(rank - 2);
            
            if (Square.IsInRange(checkRank1, file) && (board.GetPiece(checkRank1, file) == null))
            {
                potentialMoves.Add(new Move(this.Position, new Square(checkRank1, file)));
                bool isOnStartingRank = ((this.White && rank == 1) || (!this.White && rank == 6));
                if (isOnStartingRank && (board.GetPiece(checkRank2, file) == null)) // pawn can move ahead two ranks
                        potentialMoves.Add(new Move(this.Position, new Square(checkRank2, file)));
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
