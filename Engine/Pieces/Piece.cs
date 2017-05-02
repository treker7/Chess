using System;
using System.Collections.Generic;

namespace Engine
{
    internal abstract class Piece
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
        public abstract override string ToString();
    }
}