using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Pieces;

namespace Engine
{
    public class Board
    {
        public static readonly string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        private Piece[,] board = new Piece[8, 8];
        private List<Piece> pieces = new List<Piece>();

        public Board() : this(START_FEN)
        { }

        public Board(string fenStr)
        {
            string[] sections = fenStr.Split(null);
            if (sections.Length < 4 || sections.Length > 6)
                throw new ArgumentException("Bad number of sections in fen string.");

            string piecePositions = sections[0];
            string toMove = sections[1];
            string castlingAvailability = sections[2];
            string enPassantSquare = sections[3];

            if (piecePositions.Count(f => f == '/') != 7)
                throw new ArgumentException("Bad number of slaches in fen string.");
            if ((piecePositions.Count(f => f == 'k') != 1) || (piecePositions.Count(f => f == 'K') != 1))
                throw new ArgumentException("Must have one white king and one black king in fen string.");

            Piece[,] newBoard = new Piece[8, 8];
            List<Piece> newPieces = new List<Piece>();

            int row = 0, file = 0;
            foreach (char symbol in piecePositions)
            {
                if (Char.IsDigit(symbol))
                    file += (symbol - '0');
                else
                {
                    switch (Char.ToLower(symbol))
                    {
                        case 'p':
                            newBoard[row, file] = new Pawn(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case 'n':
                            newBoard[row, file] = new Knight(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case 'b':
                            newBoard[row, file] = new Bishop(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case 'r':
                            newBoard[row, file] = new Rook(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case 'q':
                            newBoard[row, file] = new Queen(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case 'k':
                            newBoard[row, file] = new King(Char.IsUpper(symbol), new Square(row, file));
                            newPieces.Add(newBoard[row, file]);
                            file++;
                            break;
                        case '/':
                            row++;
                            file = 0;
                            break;
                        default:
                            throw new ArgumentException("Unkown character in fen string: " + symbol);
                    }
                }
            }
            this.board = newBoard;
            this.pieces = newPieces;
        }

        /*
         * The symmetric board evaluation function from white's perspective
         */
        public double eval()
        {
            double eval = 0.0;
            foreach(Piece piece in pieces)
            {
                eval += (piece.White ? piece.GetValue() : -piece.GetValue());
            }
            return eval;
        }

        public override string ToString()
        {
            string fen = "";
            for(int row = 0; row < 8; row++)
            {
                int fileSkips = 0;
                for (int file = 0; file < 8; file++)
                {
                    if(board[row, file] == null)
                    {
                        fileSkips++;
                        if (fileSkips == 7)
                            fen += "8";
                    }
                    else
                    {
                        if (fileSkips != 0)
                            fen += fileSkips.ToString();
                        fen += board[row, file];
                        fileSkips = 0;
                    }
                }
                if(row != 7)
                    fen += "/";
            }
            return fen;
        }        
    }
}
