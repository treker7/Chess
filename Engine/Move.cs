using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Move
    {
        public Square From { get; }
        public Square To { get; }
        public double Eval { get; set; }

        public Move(Square from, Square to)
        {
            this.From = from;
            this.To = to;
        }

        public Move(string san) // standard algebraic notation string
        {
            if (san.Length != 4)
                throw new ArgumentException("Illegal san string.");
            this.From = new Square(san.Substring(0, 2));
            this.To = new Square(san.Substring(2, 2));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                Move other = (Move)obj;
                return (this.From.Equals(other.From) && this.To.Equals(other.To));
            }
        }
    }
}
