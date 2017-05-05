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
        public Square from { get; }
        public Square to { get; }

        public Move(Square from, Square to)
        {
            this.from = from;
            this.to = to;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                Move other = (Move)obj;
                return (this.from.Equals(other.from) && this.to.Equals(other.to));
            }
        }
    }
}
