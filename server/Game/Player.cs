using NetworkCommsDotNet.Connections;
using System;
using Protocol;
using server.Game;

namespace Game
{
    public class Player
    {
        public int Id { get; private set; }
        public String Name { get; private set; }
        public Connection Connection { get; private set; }
        public int GameId { get; set; }
        public int Team { get; set; }
        public string LastInput { get; set; }
        public Hand Hand { get; set; }
        public AnnounceElement AnnounceElement { get; set; }
        public int Score { get; set; }

        public Player(int id, String name, Connection connection)
        {
            Id = id;
            Name = name;
            Connection = connection;
            GameId = -1;
        }

        public Boolean CardCompare(Card one, Card two)
        {
            if (one.Color == two.Color && one.Value == two.Value)
                return (true);
            else
                return (false);
        }

        public void PrintHand()
        {
            Standard first = new Standard("Your cards : \n");
            Connection.SendObject("Standard", Serialization.Serialize(first).Data);
            for (int i = 0; i < Hand.Cards.Count; i++)
            {
                Standard toSend = new Standard(Hand.Cards[i].ToString());
                Connection.SendObject("Standard", Serialization.Serialize(toSend).Data);
            }
        }

        public Boolean HasCard(Card card)
        {
            for (int i = 0; i < Hand.Cards.Count; i++)
            {
                if (CardCompare(card, Hand.Cards[i]))
                    return (true);
            }
            return (false);
        }

        public Boolean HasCardInColor(String color)
        {
            for (int i = 0; i < Hand.Cards.Count; i++)
            {
                if (Hand.Cards[i].Color.Equals(color))
                    return (true);
            }
            return (false);
        }
        public void ResetPlayer()
        {
            GameId = -1;
            Hand = null;
            LastInput = null;
            AnnounceElement = null;
        }
    }
}
