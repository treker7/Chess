using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void ToStringTest()
        {
            Board checkBoard = new Board();
            Assert.AreEqual(Board.START_FEN, checkBoard.ToString());

            string checkBoardStr = "rnb1kbnr/pppp1ppp/8/4p3/4PP1q/8/PPPP2PP/RNBQKBNR w KQkq -";
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
        }

        [TestMethod()]
        public void MoveTest()
        {
            Board checkBoard = new Board();
            Assert.IsTrue(checkBoard.Move(new Move(new Square("e2"), new Square("e4"))).ToString().StartsWith("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR "));
            Assert.IsTrue(checkBoard.Move(new Move(new Square("b1"), new Square("c3"))).ToString().StartsWith("rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR "));
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
    }
}