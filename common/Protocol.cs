using System;
using System.Collections.Specialized;

namespace Protocol
{
    [Serializable]
    public class Standard
    {
        public Standard(String data)
        {
            Data = data;
        }
        public String Data { get; }
    }
    [Serializable]
    public class Greet
    {
        public Greet(String name)
        {
            Name = name;
        }
        public String Name { get; }
    }
    [Serializable]
    public class Identifier
    {
        public Identifier(int id) { Id = id; }
        public int Id { get; }
    }
    [Serializable]
    public class Game
    {
        public Game(String data)
        {
            Data = data;
        }
        public String Data { get; }
    }
}
