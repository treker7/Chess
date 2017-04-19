using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ChessBoard : Panel
    {
        private int boardLength = 600;
        public int BoardLength { get { return boardLength; } set { boardLength = value;  this.Invalidate(); } }
        public const int BOARD_SIZE = 8;
        public static readonly string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        private readonly Font pieceFont = new Font("Arial Unicode MS", 42);
        private readonly Color blackSquare = Color.FromArgb(150, 150, 150);
        private readonly Color whiteSquare = Color.FromArgb(5, 150, 5);
        private readonly Color blackPieceColor = Color.Black;
        private readonly Color whitePieceColor = Color.FromArgb(204, 204, 50);

        
        private Piece[,] board = new Piece[8, 8];
        private Piece movingPiece;
        private Point movingPiecePos;

        public ChessBoard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.MouseDown += new MouseEventHandler(this.chessBoard1_MouseDown);
            this.MouseMove += new MouseEventHandler(this.chessBoard1_MouseMove);
            this.MouseUp += new MouseEventHandler(this.chessBoard1_MouseUp);
            SetBoard(START_FEN);
        }

        public void SetBoard(string fenStr)
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
            if((piecePositions.Count(f => f == 'k') != 1) || (piecePositions.Count(f => f == 'K') != 1))
                throw new ArgumentException("Must have one white king and one black king in fen string.");

            Piece[,] newBoard = new Piece[8, 8];
            int row = 0, col = 0;
            foreach(char symbol in piecePositions)
            {
                if (Char.IsDigit(symbol))
                    col += (symbol - '0');
                else
                {
                    switch (Char.ToLower(symbol))
                    {
                        case 'p':
                        case 'n':
                        case 'b':
                        case 'r':
                        case 'q':
                        case 'k':
                            newBoard[row, col] = new Piece(Char.IsUpper(symbol), symbol);
                            col++;
                            break;
                        case '/':
                            row++;
                            col = 0;
                            break;
                        default:
                            throw new ArgumentException("Unkown character in fen string: " + symbol);
                    }
                }
            }
            board = newBoard;
            this.Refresh();
        }
                
        public void DoMove(Square s1, Square s2)
        {
            board[s2.Row, s2.Col] = board[s1.Row, s1.Col];
            board[s1.Row, s1.Col] = null;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics graphics = pe.Graphics;
            int squareSize = (BoardLength / BOARD_SIZE);
            // draw the squares on the baord
            bool isWhiteSquare = true;
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    Rectangle rectangle = new Rectangle(row * squareSize, col * squareSize, squareSize, squareSize);
                    if (isWhiteSquare)
                    {
                        graphics.FillRectangle(new SolidBrush(blackSquare), rectangle);
                    }
                    else
                    {
                        graphics.FillRectangle(new SolidBrush(whiteSquare), rectangle);
                    }
                    isWhiteSquare = !isWhiteSquare;
                }
                isWhiteSquare = (BOARD_SIZE % 2 == 0) ? !isWhiteSquare : isWhiteSquare;
            }
            // draw the chess pieces on the board
            Piece currPiece;
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    currPiece = board[row, col];
                    if (currPiece != null)
                    {
                        SolidBrush drawingBrush = currPiece.isWhite ? new SolidBrush(whitePieceColor) : new SolidBrush(blackPieceColor);
                        Point drawingPoint = new Point(col * squareSize, row * squareSize - 5);
                        graphics.DrawString(currPiece.utfDrawStr, pieceFont, drawingBrush, drawingPoint);
                    }
                }
            }
            if (movingPiece != null) // draw the moving piece
            {
                SolidBrush drawingBrush = movingPiece.isWhite ? new SolidBrush(whitePieceColor) : new SolidBrush(blackPieceColor);
                Point drawingPoint = new Point(movingPiecePos.X - 35, movingPiecePos.Y - 35);
                graphics.DrawString(movingPiece.utfDrawStr, pieceFont, drawingBrush, drawingPoint);               
            }                
        }

        private void chessBoard1_MouseDown(object sender, MouseEventArgs e)
        {
            int squareSize = (BoardLength / BOARD_SIZE);
            Square location1 = new Square(e.Y / squareSize, e.X / squareSize);

            movingPiecePos = new Point(e.X, e.Y);
            movingPiece = board[location1.Row, location1.Col];
            board[location1.Row, location1.Col] = null;
            this.Invalidate();
        }

        private void chessBoard1_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingPiece != null)
            {
                movingPiecePos = new Point(e.X, e.Y);
                this.Invalidate();
            }
        }

        private void chessBoard1_MouseUp(object sender, MouseEventArgs e)
        {
            if (movingPiece != null)
            {
                int squareSize = (BoardLength / BOARD_SIZE);
                Square location2 = new Square(e.Y / squareSize, e.X / squareSize);

                board[location2.Row, location2.Col] = movingPiece;
                movingPiece = null;
                this.Invalidate();
            }
        }
    }
}
