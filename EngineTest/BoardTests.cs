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
    public class BoardTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BoardTest()
        {
            Board failBoard = new Board("rnbnkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNZ w KQkq -");
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void BoardTest1()
        {
            Board failBoard = new Board("rnbqkbnr/ppppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            Board defaultBoard = new Board();
            // act
            string boardStr = defaultBoard.ToString();
            // assert
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -", boardStr);
        }        
    }
}