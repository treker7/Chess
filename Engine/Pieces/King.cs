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

        public override Piece MoveTo(Square to)
        {
            return new King(this.White, to);
        }

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            sbyte rank = this.Position.Rank;
            sbyte file = this.Position.File;
            
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (!((x == 1) && (y == 1)))
                    {
                        sbyte newRank = (sbyte)(rank + (x - 1));
                        sbyte newFile = (sbyte)(file + (y - 1));

                        if (Square.IsInRange(newRank, newFile) && ((board.GetPiece(newRank, newFile) == null) || (this.White != board.GetPiece(newRank, newFile).White)))
                        {
                            attacks.Add(new Square(newRank, newFile));
                        }
                    }
                }
            }
            return attacks;
        }

        public override List<Move> GetMoves(Board board)
        {
            List<Move> moves = base.GetMoves(board);

            // check for castling           
            if (board.GetCastlingAvailability(this.White ? Board.CASTLE_WK : Board.CASTLE_BK) && ClearCastle(board, true))
                moves.Add(new Move(this.Position, new Square(this.Position.Rank, 6)));
            if (board.GetCastlingAvailability(this.White ? Board.CASTLE_WQ : Board.CASTLE_BQ) && ClearCastle(board, false))
                moves.Add(new Move(this.Position, new Square(this.Position.Rank, 2)));
            
            return moves;
        }

        private bool ClearCastle(Board board, bool kingSide)
        {
            int[] fileRanges;
            if (kingSide)
                fileRanges = new int[] { 4, 6 };
            else
                fileRanges = new int[] { 2, 4 };

            List<Square> attacks = board.GetAttacksOfSide(!this.White);
            for (int checkFile = fileRanges[0]; checkFile <= fileRanges[1]; checkFile++)
            {
                if (((checkFile != 4) && (board.GetPiece(this.Position.Rank, checkFile) != null)) || (attacks.Contains(new Square(this.Position.Rank, (sbyte)checkFile))))
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            return (this.White ? "K" : "k");
        }
    }
}
