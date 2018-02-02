using NetworkCommsDotNet.Connections;
using System;

namespace server
{
    class InputHandler
    {
        public static void HandleObject(object obj, Connection connection)
        {
            Console.WriteLine("Trying to handle object");
            if (obj is Protocol.Standard)
                HandleInput((Protocol.Standard)obj, connection);
            else if (obj is Protocol.Greet)
                HandleInput((Protocol.Greet)obj, connection);
            else if (obj is Protocol.Game)
                HandleInput((Protocol.Game)obj, connection);
        }
        private static void HandleInput(Protocol.Standard standard, Connection connection)
        {
            Game.Player current = Game.PlayerHandler.GetPlayerByConnection(connection);
            Console.WriteLine("I just got a standard protocol packet");
            //Console.WriteLine("Player id : " + current.Id + " Player Name : " + current.Name);
        }
        private static void HandleInput(Protocol.Greet greet, Connection connection)
        {
            Console.WriteLine("I just got a greet protocol packet");
            Console.WriteLine("\nA greet protocol packet was received whith player name " + greet.Name);
            Game.PlayerHandler.AddPlayer(greet, connection);
        }
        private static void HandleInput(Protocol.Game game, Connection connection)
        {
            Console.WriteLine("I just got a game protocol packet\n");
            
        }
    }
}