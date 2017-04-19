using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Piece
    {        
        public readonly bool isWhite;
        public readonly char fen;
        public readonly string utfDrawStr;

        public Piece(bool isWhite, char fen)
        {
            this.isWhite = isWhite;
            this.fen = fen;

             switch (Char.ToLower(fen))
            {
                case 'p': this.utfDrawStr = "\u265F"; break;
                case 'n': this.utfDrawStr = "\u265E"; break;
                case 'b': this.utfDrawStr = "\u265D"; break;
                case 'r': this.utfDrawStr = "\u265C"; break;
                case 'q': this.utfDrawStr = "\u265B"; break;
                case 'k': this.utfDrawStr = "\u265A"; break;
                default: throw new ArgumentException("Fen string is not a valid chess piece.");
            }
        }        
    }
}
