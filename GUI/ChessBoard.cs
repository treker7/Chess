﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;
using System.Threading;

namespace GUI
{
    public partial class ChessBoard : Panel
    {
        public const int BOARD_SIZE = 8;
        public static readonly string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -";
        private int boardLength = 600;
        public int BoardLength { get { return boardLength; } set { boardLength = value;  this.Invalidate(); } }
        
        private readonly Font pieceFont = new Font("Arial Unicode MS", 42);
        private readonly Color blackSquare = Color.FromArgb(150, 150, 150);
        private readonly Color whiteSquare = Color.FromArgb(5, 150, 5);
        private readonly Color blackPieceColor = Color.Black;
        private readonly Color whitePieceColor = Color.FromArgb(204, 204, 50);
        private bool userIsWhite = true; // if the user is not playing as white, then we should flip the board 180 degrees
        
        private Piece[,] drawBoard = new Piece[8, 8];
        private Piece movingPiece;
        private Point movingPiecePos;
        private Square movingPieceFrom;

        private Board chessBoard;

        public ChessBoard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.MouseDown += new MouseEventHandler(this.chessBoard1_MouseDown);
            this.MouseMove += new MouseEventHandler(this.chessBoard1_MouseMove);
            this.MouseUp += new MouseEventHandler(this.chessBoard1_MouseUp);
            this.SetBoard(START_FEN);
        }

        public void SetBoard(string fenStr)
        {
            this.chessBoard = new Board(fenStr);
            this.drawBoard = new Piece[8, 8];

            string piecePositions = fenStr.Split(null)[0];            
            
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
                            drawBoard[row, col] = new Piece(Char.IsUpper(symbol), symbol);
                            col++;
                            break;
                        case '/':
                            row++;
                            col = 0;
                            break;                       
                    }
                }
            }
            this.Refresh();
        }        

        public void NewGame()
        {
            DialogResult playAsWhite = MessageBox.Show("Play as white?", "Play as white?", MessageBoxButtons.YesNo);
            if (playAsWhite == DialogResult.Yes)
                userIsWhite = true;
            else
                userIsWhite = false;

            this.Refresh();
            if (userIsWhite != chessBoard.WhiteMove)
                new Thread(new ThreadStart(this.PlayEngineMove)).Start();
        }

        public void PlayEngineMove()
        {
            Move engineMove = Engine.Engine.SearchMoves(chessBoard, 3);
            if (engineMove != null)
            {
                this.chessBoard = Board.Move(chessBoard, engineMove);
                this.SetBoard(chessBoard.ToString());
            }
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
                    currPiece = drawBoard[row, col];
                    if (currPiece != null)
                    {
                        SolidBrush drawingBrush = currPiece.isWhite ? new SolidBrush(whitePieceColor) : new SolidBrush(blackPieceColor);
                        Point drawingPoint = userIsWhite ? new Point(col * squareSize, row * squareSize - 5) : new Point((7 - col) * squareSize, (7 - row) * squareSize - 5);
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
            if (userIsWhite == chessBoard.WhiteMove)
            {
                int squareSize = (BoardLength / BOARD_SIZE);
                int row = e.Y / squareSize;
                int col = e.X / squareSize;
                if (userIsWhite)
                    movingPieceFrom = new Square(row, col);
                else
                    movingPieceFrom = new Square(7 - row, 7 - col);

                movingPiecePos = new Point(e.X, e.Y);
                movingPiece = drawBoard[movingPieceFrom.Row, movingPieceFrom.Col];
                drawBoard[movingPieceFrom.Row, movingPieceFrom.Col] = null;
                this.Refresh();
            }
        }

        private void chessBoard1_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingPiece != null)
            {
                movingPiecePos = new Point(e.X, e.Y);
                this.Refresh();
            }
        }

        private void chessBoard1_MouseUp(object sender, MouseEventArgs e)
        {
            if (movingPiece != null)
            {
                int squareSize = (BoardLength / BOARD_SIZE);
                int row = e.Y / squareSize;
                int col = e.X / squareSize;
                Square movingPieceTo;
                if (userIsWhite)
                    movingPieceTo = new Square(row, col);
                else
                    movingPieceTo = new Square(7 - row, 7 - col);

                Move potentialMove = new Move(new Engine.Square((sbyte)(7 - movingPieceFrom.Row), (sbyte)movingPieceFrom.Col), new Engine.Square((sbyte)(7 - movingPieceTo.Row), (sbyte)movingPieceTo.Col));
                if (Board.Move(chessBoard, potentialMove) != null) // legal move
                {
                    this.chessBoard = Board.Move(chessBoard, potentialMove);
                    this.SetBoard(chessBoard.ToString());
                    new Thread(new ThreadStart(this.PlayEngineMove)).Start();
                }
                else // illegal move
                {
                    this.drawBoard[movingPieceFrom.Row, movingPieceFrom.Col] = movingPiece;                    
                }
                this.movingPiece = null;
                this.Refresh();
            }
        }

        public override string ToString()
        {
            return chessBoard.ToString();
        }
    }
}
