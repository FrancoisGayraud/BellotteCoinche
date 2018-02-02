using System;
using System.Linq;
using NetworkCommsDotNet;
using Protocol;
using NetworkCommsDotNet.Connections;

namespace ClientApplication
{
    static class ClientCore
    {
        static string serverIP = null;
        static int serverPort = -1;
        static string playerName = null;
        static string portString = null;
        static void Main(string[] args)
        {
            InitClient(args);
            GreetServer();
            Console.WriteLine(serverIP + serverPort + playerName + "\n");
            ClientLoop();
        }
        static void ClientLoop()
        {
            while (true)
            {
                Protocol.Game toSend = new Protocol.Game(Console.ReadLine());
                if (toSend.Data.Equals("quit"))
                    break;
                else
                {
                    NetworkComms.SendObject("Game", serverIP, serverPort, Serialization.Serialize(toSend).Data);
                }
            }
            NetworkComms.Shutdown();
        }
        static void GreetServer()
        {
            Console.WriteLine("Enter your player nickname :");
            playerName = Console.ReadLine();
            Greet greeting = new Greet(playerName);
            NetworkComms.SendObject("Greet", serverIP, serverPort, Serialization.Serialize(greeting).Data);
        }

        static void InitClient(string[] args)
        {
            int tmpPort;
            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Standard", HandleStandard);
            if (args.Count() == 2)
            {
                serverIP = args[0];
                portString = args[1];
            }
            while (!(ValidateIPv4(serverIP) && int.TryParse(portString, out tmpPort)))
            {
                GetInput();
            }
            serverPort = tmpPort;
            Console.WriteLine("Write quit to quit the program.");
        }
        private static void HandleStandard(PacketHeader header, Connection connection, byte[] bytes)
        {
            object obj = Serialization.Deserialize(new Protocol.Packet { Data = bytes });
            Standard received = (Standard)obj;
            Console.WriteLine(received.Data);
        }
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }
            return splitValues.All(r => byte.TryParse(r, out byte tempForParsing));
        }
        public static void GetInput()
        {
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();
            serverIP = serverInfo.Split(':').First();
            portString = serverInfo.Split(':').Last();
        }
    }
}