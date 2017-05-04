using System;
using System.Collections.Generic;

namespace Engine
{
    // IMMUTABLE CLASS
    internal abstract class Piece : ICloneable
    {
        public Square Position { get; }
        public bool White { get; }        

        public Piece(bool white, Square position)
        {
            this.White = white;
            this.Position = position;
        }

        public abstract double GetValue();
        public abstract List<Square> GetAttacks(Board board);

        public abstract object Clone();
        public abstract override string ToString();
    }
}