using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests
{
    [TestClass()]
    public class DeckTests
    {
        private string[] Value = { "7", "8", "9", "10", "jack", "queen", "king", "ace" };
        private string[] Color = { "diamond", "spade", "heart", "club" };

        [TestMethod()]
        public void DeckTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetValueTest()
        {
            Deck deck = new Deck();
            if (deck.GetValue() != Value)
                Assert.Fail();
        }

        [TestMethod()]
        public void GetColorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PrintDeckTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DistributeTest()
        {
            Assert.Fail();
        }
    }
}