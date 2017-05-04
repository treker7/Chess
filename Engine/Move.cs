using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // IMMUTABLE CLASS
    internal class Move
    {
        public Square from { get; }
        public Square to { get; }

        public Move(Square from, Square to)
        {
            this.from = from;
            this.to = to;
        }
    }
}
