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

        private bool whiteMove; // whose move is it?
        private static readonly int CASTLE_WK = 0, CASTLE_WQ = 1, CASTLE_BK = 2, CASTLE_BQ = 3;
        private bool[] castlingAvailability = new bool[4]; // in order KQkq (white kingside, white queenside, black kingside, black queenside)
        private Square enPassantSquare; // the en passant target square
        

        public Board() : this(START_FEN)
        { }

        public Board(string fenStr)
        {
            Piece[,] newBoard = new Piece[8, 8];
            List<Piece> newPieces = new List<Piece>();
            bool newWhiteMove;
            bool[] newCastlingAbailability = new bool[4];
            Square newEnPassantSquare;

            string[] sections = fenStr.Split(null);
            if (sections.Length < 4 || sections.Length > 6)
                throw new ArgumentException("Bad number of sections in fen string.");

            string piecePositions = sections[0];
            string toMove = sections[1];
            string castlingAvailability = sections[2];
            string enPassantSquare = sections[3];

            #region Piece Positions
            if (piecePositions.Count(f => f == '/') != 7)
                throw new ArgumentException("Bad number of slaches in fen string.");
            if ((piecePositions.Count(f => f == 'k') != 1) || (piecePositions.Count(f => f == 'K') != 1))
                throw new ArgumentException("Must have one white king and one black king in fen string.");

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
                            throw new ArgumentException("Unknown character in piece positions section of fen string: " + symbol);
                    }
                }
            }
            #endregion
            #region To Move
            if (toMove.Length != 1)
                throw new ArgumentException("Error in to move section of fen string.");
            if(toMove.ToLower() == "w")
                newWhiteMove = true;            
            else if(toMove.ToLower() == "b")            
                newWhiteMove = false;
            else            
                throw new ArgumentException("Unkown character in to move section of fen string.");
            #endregion
            #region Castling Availability
            if (castlingAvailability.Length > 4)
                throw new ArgumentException("Castling availability section in fen string is too long.");

            for (int c = 0; c < newCastlingAbailability.Length; c++)
                newCastlingAbailability[c] = false;           
            if (castlingAvailability != "-")
            {
                foreach (char c in castlingAvailability)
                {
                    if (Char.IsUpper(c)) // white
                    {
                        if (c == 'K')
                            newCastlingAbailability[CASTLE_WK] = true;
                        else if (c == 'Q')
                            newCastlingAbailability[CASTLE_WQ] = true;
                        else
                            throw new ArgumentException("Unknown character in castling availability section of fen string.");
                    }
                    else // black
                    {
                        if (c == 'k')
                            newCastlingAbailability[CASTLE_BK] = true;
                        else if (c == 'q')
                            newCastlingAbailability[CASTLE_BQ] = true;
                        else
                            throw new ArgumentException("Unknown character in castling availability section of fen string.");
                    }
                }
            }
            #endregion
            #region En Passant Square
            if (enPassantSquare == "-")
                newEnPassantSquare = null;
            else
                newEnPassantSquare = new Square(enPassantSquare);
            #endregion

            // if we've gotten this far without an exception being thrown, we can safely assign new values to the member variables
            this.board = newBoard;
            this.pieces = newPieces;
            this.whiteMove = newWhiteMove;
            this.castlingAvailability = newCastlingAbailability;
            this.enPassantSquare = newEnPassantSquare;
        }

        // copy constructor
        // could also do: return new Board(this.toString())
        public Board(Board other)
        {
            for(int row = 0; row < 8; row++)
            {
                for(int file = 0; file < 8; file++)
                {
                    this.board[row, file] = (Piece)(other.board[row, file].Clone());
                }
            }
            this.pieces = new List<Piece>(other.pieces);
            this.whiteMove = other.whiteMove;
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
            fen += whiteMove ? " w " : " b ";
            if (castlingAvailability[CASTLE_WK])
                fen += "K";
            if (castlingAvailability[CASTLE_WQ])
                fen += "Q";
            if (castlingAvailability[CASTLE_BK])
                fen += "k";
            if (castlingAvailability[CASTLE_BQ])
                fen += "q";

            if (enPassantSquare == null)
                fen += " -";
            else
                fen += " " + enPassantSquare;

            return fen;
        }        
    }
}
