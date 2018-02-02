using Game;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Game
{
    class AnnounceRound
    {
        public AnnounceElement BiggestAnnounceElement { get; private set; }
        public int PassCount { get; private set; }
        public AnnounceRound()
        {
            BiggestAnnounceElement = null;
            PassCount = 0;
        }
        public void Round(Player player)
        {
            AnnounceElement announceElement = new AnnounceElement(player.LastInput, BiggestAnnounceElement == null ? 0 : BiggestAnnounceElement.Value);
            announceElement.SetValidity();
            if (announceElement.Valid)
            {
                Console.WriteLine("Got a valid announce element with value:" + announceElement.Value + " and type:" + announceElement.Type);
                player.AnnounceElement = announceElement;
                if (announceElement.Type == "pass")
                    PassCount += 1;
                else
                    PassCount = 0;
                if (BiggestAnnounceElement == null || announceElement.Value > BiggestAnnounceElement.Value)
                {
                    BiggestAnnounceElement = announceElement;
                }
            }
            else
            {
                player.AnnounceElement = null;
                Console.WriteLine("Announce Element was not valid");
            }
        }
    }
}
