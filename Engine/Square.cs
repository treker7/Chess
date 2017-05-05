using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    // IMMUTABLE CLASS
    public class Square
    {
        public sbyte File { get; }
        public sbyte Rank { get; }

        public Square(sbyte rank, sbyte file)
        {
            this.Rank = rank;
            this.File = file;            
        }

        public Square(string pgn)
        {
            if (pgn.Length != 2)
                throw new ArgumentException("Invalid alebraic notation!");

            sbyte file = (sbyte)(pgn[0] - 'a');
            sbyte rank = (sbyte)(pgn[1] - '1');

            if ((rank < 0) || (rank > 7) || (file < 0) || (file > 7))
                throw new ArgumentException("Invalid algebraic notation!");

            this.File = file;
            this.Rank = rank;
        }

        public static bool IsInRange(int rank, int file)
        {
            return ((rank > -1) && (rank < 8) && (file > -1) && (file < 8));
        }

        public override bool Equals(object obj)
        {
            return (obj != null) ? (((Square)obj).File == this.File) && (((Square)obj).Rank == this.Rank) : false;            
        }

        public override string ToString()
        {
            return (Char.ToString((char)('a' + this.File)) + Char.ToString((char)('1' + this.Rank)));
        }
    }
}
