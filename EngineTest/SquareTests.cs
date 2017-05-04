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
    public class SquareTests
    {
        [TestMethod()]
        public void SquareTest1()
        {
            Square square = new Square("a8");
            Assert.AreEqual(7, square.Rank);
            Assert.AreEqual(0, square.File);
        }

        [TestMethod()]
        public void SquareTest2()
        {
            try
            {
                new Square("a9");
                new Square("a0");
                new Square("a11");
                new Square("i1");
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}