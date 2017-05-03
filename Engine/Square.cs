using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    internal sealed class Square
    {
        public int File { get; }
        public int Row { get; }

        public Square(int row, int col)
        {
            this.File = row;
            this.Row = col;
        }

        public Square(string pgn)
        {
            if (pgn.Length != 2)
                throw new ArgumentException("Invalid alebraic notation!");

            int col = pgn[0] - 'a';
            int row = (7 - (pgn[1] - '0')) + 1;

            if ((row < 0) || (row > 7) || (col < 0) || (col > 7))
                throw new ArgumentException("Invalid algebraic notation!");

            this.File = row;
            this.Row = col;
        }

        public override string ToString()
        {
            return (Char.ToString((char)('a' + this.File)) + Char.ToString((char)('1' + this.Row)));
        }
    }
}
