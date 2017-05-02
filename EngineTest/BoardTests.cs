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
        public void ToStringTest()
        {
            // arrange
            Board defaultBoard = new Board();
            // act
            string boardStr = defaultBoard.ToString();
            // assert
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", boardStr);
        }
    }
}