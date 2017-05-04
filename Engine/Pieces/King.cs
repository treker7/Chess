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

        public override List<Square> GetAttacks(Board board)
        {
            List<Square> attacks = new List<Square>();
            char rank = this.Position.Rank;
            char file = this.Position.File;
            
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (!((x == 1) && (y == 1)))
                    {
                        char newRank = (char)(rank + (x - 1));
                        char newFile = (char)(file + (y - 1));

                        if (Square.IsInRange(newRank, newFile) && ((board.GetPiece(newRank, newFile) == null) || (this.White != board.GetPiece(newRank, newFile).White)))
                        {
                            attacks.Add(new Square(newRank, newFile));
                        }
                    }
                }
            }
            return attacks;
        }

        public override string ToString()
        {
            return (this.White ? "K" : "k");
        }
    }
}
