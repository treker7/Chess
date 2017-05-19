using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Pieces
{
    class Pawn : Piece
    {
        public static readonly int VALUE = 100;

        // the relative values of squares for pawns to occupy (from white's perspective)
        private static readonly int[,] positionalMatrix = {
            { 0,  0,  0,  0,  0,  0,  0,  0 },
            { 5, 10, 10,-20,-20, 10, 10, 5 },
            { 5, -5,-10,  0,  0,-10, -5,  5 },
            { 0,  0,  0, 20, 20,  0,  0,  0 },
            { 5,  5, 10, 25, 25, 10,  5,  5 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            { 50, 50, 50, 50, 50, 50, 50, 50},
            { 0,  0,  0,  0,  0,  0,  0,  0 }
        };

        public Pawn(bool white, Square position) : base(white, position)
        { }

        public override int GetValue(Board board)
        {
            int value = Pawn.VALUE;
            // positional consideration          
            if (this.White)
                value += positionalMatrix[this.Position.Rank, this.Position.File];
            else
                value += positionalMatrix[(7 - this.Position.Rank), this.Position.File];

            return value;
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
