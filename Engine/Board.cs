using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Pieces;

namespace Engine
{
    // IMMUTABLE CLASS
    public class Board
    {
        public static readonly string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -";

        private Piece[,] board = new Piece[8, 8];
        private List<Piece> pieces = new List<Piece>();
        private King whiteKing, blackKing;

        private bool whiteMove; // whose move is it?
        private static readonly int CASTLE_WK = 0, CASTLE_WQ = 1, CASTLE_BK = 2, CASTLE_BQ = 3;
        private bool[] castlingAvailability = new bool[4]; // in order KQkq (white kingside, white queenside, black kingside, black queenside)
        private Square enPassantSquare; // the en passant target square        

        public Board() : this(START_FEN)
        { }

        public Board(string fenStr)
        {            
            string[] sections = fenStr.Split(null);
            if (sections.Length < 4 || sections.Length > 6)
                throw new ArgumentException("Bad number of sections in fen string.");

            string piecePositions = sections[0];
            string toMove = sections[1];
            string castling = sections[2];
            string enPassant = sections[3];

#region Piece Positions
            if (piecePositions.Count(f => f == '/') != 7)
                throw new ArgumentException("Bad number of slaches in fen string.");
            if ((piecePositions.Count(f => f == 'k') != 1) || (piecePositions.Count(f => f == 'K') != 1))
                throw new ArgumentException("Must have one white king and one black king in fen string.");

            char rank = (char)7, file = (char)0;
            foreach (char symbol in piecePositions)
            {
                if (Char.IsDigit(symbol))
                    file += (char)(symbol - '0');
                else
                {
                    switch (Char.ToLower(symbol))
                    {
                        case 'p':
                            board[rank, file] = new Pawn(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            file++;
                            break;
                        case 'n':
                            board[rank, file] = new Knight(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            file++;
                            break;
                        case 'b':
                            board[rank, file] = new Bishop(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            file++;
                            break;
                        case 'r':
                            board[rank, file] = new Rook(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            file++;
                            break;
                        case 'q':
                            board[rank, file] = new Queen(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            file++;
                            break;
                        case 'k':
                            board[rank, file] = new King(Char.IsUpper(symbol), new Square(rank, file));
                            pieces.Add(board[rank, file]);
                            if (Char.IsUpper(symbol))
                                whiteKing = (King)board[rank, file];
                            else
                                blackKing = (King)board[rank, file];
                            file++;
                            break;
                        case '/':
                            rank--;
                            file = (char)0;
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
                this.whiteMove = true;            
            else if(toMove.ToLower() == "b")            
                this.whiteMove = false;
            else            
                throw new ArgumentException("Unkown character in to move section of fen string.");
            #endregion
#region Castling Availability
            if (castling.Length > 4)
                throw new ArgumentException("Castling availability section in fen string is too long.");

            for (int c = 0; c < castlingAvailability.Length; c++)
                this.castlingAvailability[c] = false;           
            if (castling != "-")
            {
                foreach (char c in castling)
                {
                    if (Char.IsUpper(c)) // white
                    {
                        if (c == 'K')
                            this.castlingAvailability[CASTLE_WK] = true;
                        else if (c == 'Q')
                            this.castlingAvailability[CASTLE_WQ] = true;
                        else
                            throw new ArgumentException("Unknown character in castling availability section of fen string.");
                    }
                    else // black
                    {
                        if (c == 'k')
                            this.castlingAvailability[CASTLE_BK] = true;
                        else if (c == 'q')
                            this.castlingAvailability[CASTLE_BQ] = true;
                        else
                            throw new ArgumentException("Unknown character in castling availability section of fen string.");
                    }
                }
            }
            #endregion
#region En Passant Square
            if (enPassant == "-")
                this.enPassantSquare = null;
            else
                this.enPassantSquare = new Square(enPassant);
            #endregion            
        }

        // copy constructor
        // could also do: return new Board(this.toString())
        public Board(Board other)
        {
            for(int rank = 0; rank < 8; rank++)
            {
                for(int file = 0; file < 8; file++)
                {
                    this.board[rank, file] = (Piece)other.board[rank, file];
                    this.pieces.Add(board[rank, file]);
                    if(board[rank, file] is King)
                    {
                        if (board.ToString() == "K")
                            this.whiteKing = (King)board[rank, file];
                        else
                            this.blackKing = (King)board[rank, file];
                    }
                }
            }
            this.whiteMove = other.whiteMove;
            this.castlingAvailability = (bool[])other.castlingAvailability.Clone();
            this.enPassantSquare = other.enPassantSquare;
        }

        internal Piece GetPiece(int rank, int file)
        {
            return board[rank, file];
        }

        /*
         * The symmetric board evaluation function from white's perspective
         */
        public double Eval()
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
            for(int rank = 7; rank > -1; rank--)
            {
                int fileSkips = 0;
                for (int file = 0; file < 8; file++)
                {
                    if(board[rank, file] == null)
                    {
                        fileSkips++;
                        if (fileSkips == 7)
                            fen += "8";
                    }
                    else
                    {
                        if (fileSkips != 0)
                            fen += fileSkips.ToString();
                        fen += board[rank, file];
                        fileSkips = 0;
                    }
                }
                if(rank != 0)
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
                fen += " " + enPassantSquare.ToString();

            return fen;
        }        
    }
}
