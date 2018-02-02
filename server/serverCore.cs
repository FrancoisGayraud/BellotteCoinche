using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Game;

namespace server
{
    class ServerCore
    {
        static void Main(string[] args)
        {
            InitServer();
        }
        private static void InitServer()
        {
            NetworkComms.AppendGlobalConnectionCloseHandler(ClientDisconnect);
            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Standard", HandleStandard);
            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Greet", HandleGreet);
            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Game", HandleGame);
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8080));
            Console.WriteLine("Server listening for TCP connection on:");
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);
            NetworkComms.Shutdown();
        }
        private static void HandleStandard(PacketHeader header, Connection connection, byte[] bytes)
        {
            Console.WriteLine("Got an standard packet !");
            object obj = Protocol.Serialization.Deserialize(new Protocol.Packet { Data = bytes });
            Player current = PlayerHandler.GetPlayerByConnection(connection);
            if (current != null)
                Console.WriteLine("Player id : " + current.Id + " Player Name : " + current.Name);
            else
                Console.WriteLine("No player found with that connection.");
        }
        private static void HandleGreet(PacketHeader header, Connection connection, byte[] bytes)
        {
            Console.WriteLine("Got an greet packet !");
            object obj = Protocol.Serialization.Deserialize(new Protocol.Packet { Data = bytes });
            Protocol.Greet greet = (Protocol.Greet)obj;
            Console.WriteLine("connection info" + connection.ToString());
            Console.WriteLine("A greet protocol packet was received whith player name " + greet.Name);
            PlayerHandler.AddPlayer(greet, connection);
            GameHandler.NeedNewGame();
        }
        private static void HandleGame(PacketHeader header, Connection connection, byte[] bytes)
        {
            Console.WriteLine("Got a game packet !");
            object obj = Protocol.Serialization.Deserialize(new Protocol.Packet() { Data = bytes });
            Protocol.Game game = (Protocol.Game)obj;
            Console.WriteLine("Game info " + connection.ToString());
            Console.WriteLine("Packet contain : " + game.Data);
            Player player = PlayerHandler.GetPlayerByConnection(connection);
            player.LastInput = game.Data;
            if (player.GameId != (-1))
            {
                GameHandler.GetGameById(player.GameId).HandleTurn(player);
            }
        }
        private static void ClientDisconnect(Connection connection)
        {
            int playerId = PlayerHandler.GetPlayerByConnection(connection).Id;
            int playerGameId = PlayerHandler.GetPlayerById(playerId).GameId;
            Console.WriteLine("gameid : " + PlayerHandler.GetPlayerByConnection(connection).GameId);
            if (playerGameId != -1)
                GameHandler.GetGameById(playerGameId).RemovePlayer(PlayerHandler.GetPlayerByConnection(connection));
            PlayerHandler.RemovePlayerById(playerId);
            if (playerGameId != -1)
                GameHandler.DestroyGameById(playerGameId);
            Console.WriteLine("A client has disconnect");
        }
    }
}
