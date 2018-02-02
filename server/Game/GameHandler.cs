using System;
using System.Collections.Generic;
using System.Linq;
using NetworkCommsDotNet.Connections;
using Protocol;

namespace Game
{
	public static class GameHandler
	{
        private static List<Game> Games = new List<Game>();
        private static int Id = 0;
        public static void AddGame(List<Player> players) 
        {
            Games.Add(new Game(Id, players));
            Id++;
        }

        public static void NeedNewGame()
        {
            List<Player> players;
            players = PlayerHandler.ArePlayerReady();
            Console.WriteLine("number of player ready : " + players.Count);
            if (players.Count == 4)
            {
                players.ForEach(delegate (Player player)
                {
                    player.GameId = Id;
                    Console.WriteLine("player : " + player.Id + " has joined a game");
                });
                AddGame(players);
            }
        }

        public static void DestroyGameById(int id)
        {
            Games.Find(x => x.Id == id).EndGame();
            Games.Remove(Games.Single(x => x.Id == id));
            Console.WriteLine("A game has stop : " + id);
        }

        public static Game GetGameById(int id) => Games.Find(x => x.Id.Equals(id));

    }
}