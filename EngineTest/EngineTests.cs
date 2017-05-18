using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        [TestMethod()]
        public void SearchMovesTest()
        {
            // mate in 1 puzzles
            Board testBoard = new Board("r1b3Q1/1pp4p/p4p2/5k2/8/8/PP3PPP/R1B1R1K1 w - - 0 1");
            Assert.AreEqual(new Move("g2g4"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("5k2/5p2/5P2/8/8/2r5/2rR2K1/4B2R w - - 0 1");
            Assert.AreEqual(new Move("h1h8"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("8/4R3/7p/6p1/6q1/5kP1/7P/4K2R w K - 0 1");
            Assert.AreEqual(new Move("e1g1"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("N1bq1b1r/3pkppp/1pp5/7Q/4P3/8/PPPP1PPP/RNB1KB1R w KQ - 0 1");
            Assert.AreEqual(new Move("h5e5"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("8/8/1P6/5n2/8/7k/6r1/7K b - - 0 1");
            Assert.AreEqual(new Move("f5g3"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("5k2/6b1/8/8/1n6/p7/8/1KR5 b - - 0 1");
            Assert.AreEqual(new Move("a3a2"), Engine.SearchMoves(testBoard, 1));
            testBoard = new Board("1b6/8/8/8/8/8/8/3n1k1K b - - 0 1");
            Assert.AreEqual(new Move("d1f2"), Engine.SearchMoves(testBoard, 1));

            // mate in 2 puzzles
            testBoard = new Board("R4r1k/1p3qpp/8/8/8/1PQ5/1B3PPP/6K1 w - - 0 1"); // white to move
            Assert.AreEqual(new Move("c3g7"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("rnbq2kr/ppp3pp/4P2n/3p2NQ/4p3/B1P5/P1P2PPP/R3KB1R w - - 0 1");
            Assert.AreEqual(new Move("h5f7"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("R1Q5/1p3p2/1k1qpb2/8/P2p4/P2P2P1/4rPK1/8 w - - 0 1");
            Assert.AreEqual(new Move("a4a5"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("r1bq2k1/ppp2r1p/2np1pNQ/2bNpp2/2B1P3/3P4/PPP2PPP/R3K2R w KQ - 0 1");
            Assert.AreEqual(new Move("d5f6"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("r2q1k1r/ppp1n1Np/1bnpB2B/8/1P1pb1P1/2P4P/P4P2/R2Q1RK1 w - - 0 1");
            Assert.AreEqual(new Move("g7h5"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("r4r2/pQ3ppp/2np4/2bk4/5P2/6P1/PPP5/R1B1KB1q w Q - 0 1");
            Assert.AreEqual(new Move("b7b3"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("r1b1kbnr/pp2qp2/1np4p/4P3/2B2BpN/2NQ1pP1/PPP4P/2KR3R w kq - 0 1");
            Assert.AreEqual(new Move("c4f7"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("4q1k1/6p1/4rp2/8/7Q/8/5PPP/3RR1K1 b - - 0 1"); // black to move
            Assert.AreEqual(new Move("e6e1"), Engine.SearchMoves(testBoard, 3));
            testBoard = new Board("7r/3k1pp1/4p3/p3P3/2q5/K5Pp/7P/8 b - - 2 46");
            Assert.AreEqual(new Move("h8b8"), Engine.SearchMoves(testBoard, 3));

            // mate in 3 puzzles
            testBoard = new Board("8/pk1B4/p7/2K1p3/8/8/4Q3/8 w - - 0 1"); // white to move
            Assert.AreEqual(new Move("d7h3"), Engine.SearchMoves(testBoard, 5));            
            testBoard = new Board("4r1b1/1p4B1/pN2pR2/RB2k3/1P2N2p/2p3b1/n2P1p1r/5K1n w - - 0 1");
            Assert.AreEqual(new Move("f1e2"), Engine.SearchMoves(testBoard, 5));
            //slow
            //testBoard = new Board("5r1k/2r2P2/3qbRPp/1p1n4/p2pB3/4p1NP/PP6/6RK w - - 0 1");
            //Assert.AreEqual(new Move("g6g7"), Engine.SearchMoves(testBoard, 5));
            testBoard = new Board("r4r1k/ppp3pp/3pb3/6K1/8/8/PB3bPP/RN1Q3R b - - 0 1"); // black to move
            Assert.AreEqual(new Move("f8f5"), Engine.SearchMoves(testBoard, 5));
        }
    }
}