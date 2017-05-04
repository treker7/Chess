using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    // IMMUTABLE CLASS
    public class Square
    {
        public char File { get; }
        public char Rank { get; }

        public Square(char rank, char file)
        {
            this.File = rank;
            this.Rank = file;
        }

        public Square(string pgn)
        {
            if (pgn.Length != 2)
                throw new ArgumentException("Invalid alebraic notation!");

            char file = (char)(pgn[0] - 'a');
            char rank = (char)(pgn[1] - '1');

            if ((rank < 0) || (rank > 7) || (file < 0) || (file > 7))
                throw new ArgumentException("Invalid algebraic notation!");

            this.File = file;
            this.Rank = rank;
        }

        public static bool IsInRange(char rank, char file)
        {
            return ((rank > -1) || (rank < 8) || (file > -1) || (file < 8));
        }

        public override string ToString()
        {
            return (Char.ToString((char)('a' + this.File)) + Char.ToString((char)('1' + this.Rank)));
        }
    }
}
