using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    class PlayerHandler
    {
        static private List<Player> Players = new List<Player>();
        static private int nextId;
        public PlayerHandler()
        {
            nextId = 0;
        }
        public static void AddPlayer(Protocol.Greet greet, Connection connection)
        {
            Players.Add(new Player(nextId, greet.Name, connection));
            nextId += 1;
        }

        public static Player GetPlayerById(int id)
        {
            return Players.Find(x => x.Id.Equals(id));
        }

        public static Player GetPlayerByConnection (Connection connection)
        {
            return Players.Find(x => x.Connection.ToString().Equals(connection.ToString()));
        }

        public static List<Player> getPlayersByGameId(int gameId)
        {
            return (Players.FindAll(x => x.GameId.Equals(gameId)));
        }

        public static List<Player> ArePlayerReady()
        {
            return (getPlayersByGameId(-1));
        }

        public static void RemovePlayerById(int id)
        {
            Players.Remove(Players.Single(x => x.Id == id));
        }
    }
}
