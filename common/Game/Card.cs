using System;

namespace Game
{
    public class Card
    {

        public Card(string color, string value)
        {
            Color = color;
            Value = value;
            for (int i = 0; i < 8; i++)
            {
                if (Value.Equals(Val[i]))
                    Strength = i;
            }
        }
        
        public string Color { get; }
        public string Value { get; }
        public int Strength { get; private set; }
        private String[] Val = { "7", "8", "9", "10", "jack", "queen", "king", "ace"};

        public override string ToString()
        {
            return Value + " " + Color + "\n";
        }
    }
}