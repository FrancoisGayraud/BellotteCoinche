using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Game
{
    public class Deck
    {
	    public Deck()
	    {
		    for (int i = 0; i < 4; i++)
		    {
			    for (int u = 0; u < 8; u++)
			    {
				    Cards.Add(new Card(Color[i], Value[u]));
			    }
		    }
            Cards = ShuffleList(Cards);
            PrintDeck();
	    }

	    private List<Card> Cards = new List<Card>();
	    private string[] Value = {"7", "8", "9", "10", "jack", "queen", "king", "ace"};
	    private string[] Color = {"diamond", "spade", "heart", "club"};

	    public string[] GetValue()
	    {
		    return Value;
	    }

	    public string[] GetColor()
	    {
		    return Color;
	    }

	    public void PrintDeck()
	    {
		    for (int i = 0; i < Cards.Count; i++)
		    {
			    Console.WriteLine(Cards[i].ToString());
		    }
	    }

        public Hand Distribute()
        {
            List<Card> tmp = new List<Card>();
            Hand ret = new Hand();

            for (int i = 0; i < 8; i++)
            {
                tmp.Add(Cards[0]);
                Cards.RemoveAt(0);
            }
            ret.Cards = tmp;
            return (ret);
        }

        public static List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }
            return randomList;
        }
    }
}