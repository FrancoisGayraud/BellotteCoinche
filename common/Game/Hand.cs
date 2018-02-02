using System;
using System.Collections.Generic;

namespace Game
{
    public class Hand
    {
        public List<Card> Cards { get; set; }

        public void RemoveFirstCard()
        {
            if (Cards.Count > 0)
            {
                Cards.RemoveAt(0);
            }
        }

        public void PrintHand()
        {
            int i = 0;
            
            Console.WriteLine("Your hand : \n");
            while (i < Cards.Count)
            {
                Console.WriteLine(Cards[i].ToString() + "\n");   
                i++;
            }   
        }

        public void RemoveCard(String color, String value)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Color.Equals(color) && Cards[i].Value.Equals(value))
                    Cards.RemoveAt(i);
            }
        }

    }
}