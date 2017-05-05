using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // IMMUTABLE CLASS
    public class Move
    {
        public Square From { get; }
        public Square To { get; }

        public Move(Square from, Square to)
        {
            this.From = from;
            this.To = to;
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
