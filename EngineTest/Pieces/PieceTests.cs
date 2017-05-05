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
    public class PieceTests
    {
        [TestMethod()]
        public void GetMovesTest()
        {
            // cannot move into check
            Board checkBoard = new Board("rnbq3r/pppp1kpp/5n2/2b1p3/4P2N/8/PPPP1PPP/RNBQK2R b KQ - 1 5");
            Piece blackKing = checkBoard.GetKingOfSide(false);
            List<Move> moves = blackKing.GetMoves(checkBoard);
            Assert.IsFalse(moves.Contains(new Move(new Square("f7"), new Square("g6"))));

            checkBoard = new Board("rnbqkbnr/pppp1ppp/8/4p2Q/8/4P3/PPPP1PPP/RNB1KBNR b KQkq - 1 2");            
            Assert.IsFalse(checkBoard.GetMovesOfSide(false).Contains(new Move(new Square("f7"), new Square("f6"))));            
        }
    }
}