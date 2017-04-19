using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Square
    {        
        public int Row { get; }
        public int Col { get; }

        public Square(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public Square(string pgn)
        {
            if (pgn.Length != 2)
                throw new ArgumentException("Invalid alebraic notation!");

            int col = pgn[0] - 'a';
            int row = (7 - (pgn[1] - '0')) + 1;

            if ((row < 0) || (row > 7) || (col < 0) || (col > 7))
                throw new ArgumentException("Invalid algebraic notatoin!");

            this.Row = row;
            this.Col = col;
        }
    }    
}
