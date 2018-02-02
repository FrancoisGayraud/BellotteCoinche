using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Protocol
{
    public class Packet
    {
        public byte[] Data { get; set; }
    }
    public class Serialization
    {
        public static Packet Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return new Packet { Data = memoryStream.ToArray() };
            }
        }

        public static object Deserialize(Packet packet)
        {
            using (var memoryStream = new MemoryStream(packet.Data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }
    }
}
