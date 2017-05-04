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
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -", checkBoard.ToString());

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
        }
    }
}