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
        public void GetValueTest()
        {
            Deck deck = new Deck();
            CollectionAssert.AreEqual(deck.GetValue(), Value);
        }

        [TestMethod()]
        public void GetColorTest()
        {
            Deck deck = new Deck();
            CollectionAssert.AreEqual(deck.GetColor(), Color);
        }
    }
}