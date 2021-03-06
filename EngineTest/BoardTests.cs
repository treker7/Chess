﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Tests
{

    /*
     * Note: easily get fen strings from board positions online here: https://www.chess.com/analysis-board-editor
     */
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BoardTest1()
        {
            Board failBoard = new Board("rnbnkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNZ w KQkq -");
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void BoardTest2()
        {
            Board failBoard = new Board("rnbqkbnr/ppppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BoardTest3()
        {
            // cannot be in check if it's not your move
            Board failBoard = new Board("rnbqkbnr/pppp2pp/5p2/4p2Q/3PP3/8/PPP2PPP/RNB1KBNR w KQkq - 1 3");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Board checkBoard = new Board();
            Assert.AreEqual(Board.START_FEN, checkBoard.ToString());

            string checkBoardStr = "rnb1kbnr/pppp1ppp/8/4p3/4PP1q/8/PPPP2PP/RNBQKBNR w KQkq -";
            checkBoard = new Board(checkBoardStr);
            Assert.AreEqual(checkBoardStr, checkBoard.ToString());

            checkBoardStr = "rnb2rk1/p1p1q1pp/1p3p2/Q1bppN2/2BPPn2/5P2/PPP3PP/RNB2RK1 w - -";
            checkBoard = new Board(checkBoardStr);
            Assert.AreEqual(checkBoardStr, checkBoard.ToString());
        }

        [TestMethod()]
        public void IsInCheckTest()
        {
            // no checks
            Board checkBaord = new Board();
            Assert.IsFalse(checkBaord.IsInCheck(true));
            Assert.IsFalse(checkBaord.IsInCheck(false));

            // no checks
            checkBaord = new Board("rnb1kbnr/pp1p1p1p/1q4p1/2p1p3/3PP3/3QB3/PPP2PPP/RN2KBNR w KQkq - 2 5");
            Assert.IsFalse(checkBaord.IsInCheck(true));
            Assert.IsFalse(checkBaord.IsInCheck(false));

            // white in check
            checkBaord = new Board("rnb1kbnr/pppp1ppp/8/4p3/4PP1q/8/PPPP2PP/RNBQKBNR w KQkq - 1 3");
            Assert.IsTrue(checkBaord.IsInCheck(true));
            Assert.IsFalse(checkBaord.IsInCheck(false));

            // black in check
            checkBaord = new Board("rnbqk2r/pppp1Bpp/5n2/2b1p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 4");
            Assert.IsFalse(checkBaord.IsInCheck(true));
            Assert.IsTrue(checkBaord.IsInCheck(false));

            //black in check
            checkBaord = new Board("rnbq3r/pppp2pp/5nk1/2b1p3/4P2N/8/PPPP1PPP/RNBQK2R b KQ -");
            Assert.IsFalse(checkBaord.IsInCheck(true));
            Assert.IsTrue(checkBaord.IsInCheck(false));
        }

        [TestMethod()]
        public void GetMovesOfSideTest()
        {
            Board checkBoard = new Board();
            List<Move> firstMoves = new List<Move>();
            Assert.IsTrue(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("e2"), new Square("e4"))));
            Assert.IsTrue(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("e2"), new Square("e3"))));
            Assert.IsTrue(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("g1"), new Square("h3"))));
            Assert.IsTrue(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("g1"), new Square("f3"))));

            // cannot move into check
            checkBoard = new Board("rnbq3r/pppp1kpp/5n2/2b1p3/4P2N/8/PPPP1PPP/RNBQK2R b KQ - 1 5");
            Assert.IsFalse(checkBoard.GetKingOfSide(false).GetMoves(checkBoard).Contains(new Move(new Square("f7"), new Square("g6"))));
            checkBoard = new Board("rnbqkbnr/pppp1ppp/8/4p2Q/8/4P3/PPPP1PPP/RNB1KBNR b KQkq - 1 2");
            Assert.IsFalse(checkBoard.GetMovesOfSide(false).Contains(new Move(new Square("f7"), new Square("f6"))));
            checkBoard = new Board("rnbq1bnr/pppp1ppp/4k3/4p3/4P3/4K3/PPPP1PPP/RNBQ1BNR w - - 4 4");
            Assert.IsFalse(checkBoard.GetKingOfSide(true).GetMoves(checkBoard).Contains(new Move(new Square("e3"), new Square("f4"))));

            // both sides CAN castle
            checkBoard = new Board("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 4 4");
            Assert.IsTrue(checkBoard.GetKingOfSide(true).GetMoves(checkBoard).Contains(new Move(new Square("e1"), new Square("g1"))));
            Assert.IsTrue(checkBoard.GetKingOfSide(false).GetMoves(checkBoard).Contains(new Move(new Square("e8"), new Square("g8"))));

            // neither side can castle
            checkBoard = new Board();
            Assert.IsFalse(checkBoard.GetKingOfSide(true).GetMoves(checkBoard).Contains(new Move(new Square("e1"), new Square("g1"))));
            Assert.IsFalse(checkBoard.GetKingOfSide(false).GetMoves(checkBoard).Contains(new Move(new Square("e8"), new Square("g8"))));
            checkBoard = new Board("rnbqk2r/pppp2pp/5p2/2b1p2n/2B1P2N/5P2/PPPP2PP/RNBQK2R w KQkq - 0 6");
            Assert.IsFalse(checkBoard.GetKingOfSide(true).GetMoves(checkBoard).Contains(new Move(new Square("e1"), new Square("g1"))));
            Assert.IsFalse(checkBoard.GetKingOfSide(false).GetMoves(checkBoard).Contains(new Move(new Square("e8"), new Square("g8"))));

            // en Passant test
            checkBoard = new Board("rnbq2nr/ppppk1p1/B3p2p/4Pp2/8/b7/PPPPKPPP/RNBQ2NR w - f6 0 6"); // white en passant
            Assert.IsTrue(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("e5"), new Square("f6"))));
            Assert.IsFalse(checkBoard.GetMovesOfSide(false).Contains(new Move(new Square("e5"), new Square("f6"))));
            checkBoard = new Board("rnbqkbnr/pppp1ppp/B7/8/4pP2/2N1P3/PPPP2PP/R1BQK1NR b KQkq f3 0 4"); // black en passant
            Assert.IsTrue(checkBoard.GetMovesOfSide(false).Contains(new Move(new Square("e4"), new Square("f3"))));
            Assert.IsFalse(checkBoard.GetMovesOfSide(true).Contains(new Move(new Square("e4"), new Square("f3"))));
        }

        [TestMethod()]
        public void MoveTest()
        {
            // legal moves
            Board checkBoard = new Board();
            Assert.IsTrue(Board.Move(checkBoard, new Move(new Square("e2"), new Square("e4"))).ToString().StartsWith("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR "));
            Assert.IsTrue(Board.Move(checkBoard, new Move(new Square("b1"), new Square("c3"))).ToString().StartsWith("rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR "));

            // illegal moves
            checkBoard = new Board("rnb2rk1/p1p1q3/1p2nppp/1Q1pPN2/1bB1P3/5PPP/PPP5/RNB2RK1 w - - 0 15");
            Assert.AreSame(null, Board.Move(checkBoard, new Move(new Square("c4"), new Square("c3"))));
            Assert.AreSame(null, Board.Move(checkBoard, new Move(new Square("f1"), new Square("c1"))));
            Assert.AreSame(null, Board.Move(checkBoard, new Move(new Square("g8"), new Square("g7"))));

            // pawn promotion test
            checkBoard = new Board("rnbqk2r/pppp2P1/5p2/1B2N2p/1b2n3/8/PPPP2PP/RNBQK2R w KQkq - 0 9");
            checkBoard = Board.Move(checkBoard, new Move(new Square("g7"), new Square("g8")));
            Assert.AreEqual("Q", checkBoard.GetPiece(new Square("g8")).ToString());
        }

        [TestMethod()]
        public void GetStatusTest()
        {
            // NORMAL TESTS
            Board checkBoard = new Board();
            Assert.AreEqual(Board.STATUS_NORMAL, checkBoard.GetStatus(false));
            Assert.AreEqual(Board.STATUS_NORMAL, checkBoard.GetStatus(true));
            checkBoard = new Board("rnbq3r/pppp1kpp/5n2/2b1p3/4P2N/8/PPPP1PPP/RNBQK2R b KQ - 1 5");
            Assert.AreEqual(Board.STATUS_NORMAL, checkBoard.GetStatus(false));
            Assert.AreEqual(Board.STATUS_NORMAL, checkBoard.GetStatus(true));

            // CHECKMATE TESTS
            checkBoard = new Board("rnbqk1nr/pp1p1Qpp/2p5/2b1p3/2B5/4P3/PPPP1PPP/RNB1K1NR b KQkq - 0 4");
            Assert.AreEqual(Board.STATUS_CHECKMATE, checkBoard.GetStatus(false));

            // STALEMATE TESTS
            checkBoard = new Board("6k1/2R5/5Q2/8/7P/1P2P3/P4P1P/5KNR b - - 4 34");
            Assert.AreEqual(Board.STATUS_STALEMATE, checkBoard.GetStatus(false));
        }

        [TestMethod()]
        public void EvalTest()
        {
            // white is winning
            Board checkBoard = new Board("N1bq1b1r/3pkppp/1pp5/7Q/4P3/8/PPPP1PPP/RNB1KB1R w KQ - 0 1");
            Assert.IsTrue(checkBoard.Eval() > 0);

            // black is winning
            checkBoard = new Board("rnbqkbnr/pp1p2pp/8/8/8/2PBB2N/PP4PP/RN2K2R b KQ - 2 11");
            Assert.IsTrue(checkBoard.Eval() < 0);
        }
    }
}