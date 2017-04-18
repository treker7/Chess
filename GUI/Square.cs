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
    }    
}
