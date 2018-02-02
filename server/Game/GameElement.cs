using System;
using System.Collections.Generic;
using Game;
using Protocol;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Game
{
    public class GameElement
    {
        public String Input { get; set; }
        public List<Card> RoundCard = new List<Card>();
        static List<string> Colors = new List<string>(new string[] { "diamond", "club", "heart", "spade" });
        static List<string> Values = new List<string>(new string[] { "7", "8", "9", "10", "jack", "queen", "king", "ace"});
        public String ColorInput { get; private set; }
        public String ValueInput { get; private set; }

        public GameElement(String input, List<Card> roundCard)
        {
            Input = input.Trim();
            RoundCard = roundCard;
        }

        public Boolean CheckColorInput()
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                if (Colors[i].Equals(ColorInput))
                    return (true);
            }
            return (false);
        }

        public String GetColorOfTheRound()
        {
            return (RoundCard[0].Color);
        }

        public Boolean CheckValueInput()
        {
            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].Equals(ValueInput))
                    return (true);
            }
            return (false);
        }

        public Boolean CheckInput()
        {
            if (Input.Count(x => x == ' ') != 1)
            {
                Console.WriteLine("IF");
                return (false);
            }
            String[] result;
            String[] sep = new string[] { " " };
            result = Input.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("nb of arg : " + result.Length);
            if (result.Length != 2)
                return (false);
            ColorInput = result[0];
            ValueInput = result[1];
            if (!CheckColorInput())
                return (false);
            else if (!CheckValueInput())
                return (false);
            return (true);
        }

        public Boolean CheckHasCard(Card card, Player player)
        {
            return (player.HasCard(card));
        }

        public Boolean CheckColor(Card card)
        {
            if (card.Color == RoundCard[0].Color)
                return (true);
            else
                return (false);
        }

        public Boolean CheckIfPlayerHasColor(Player player)
        {
            return (player.HasCardInColor(RoundCard[0].Color));
        }

    }
}
