using System;
using System.Collections.Generic;

namespace Engine
{
    internal abstract class Piece : ICloneable
    {
        public bool White { get; }
        public Square Position { get; }

        public Piece(bool white, Square position)
        {
            this.White = white;
            this.Position = position;
        }

        public abstract double GetValue();
        public abstract List<Square> GetMoves();

        public abstract object Clone();
        public abstract override string ToString();        
    }
}