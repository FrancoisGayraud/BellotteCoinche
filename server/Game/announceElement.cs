using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Game
{
    public class AnnounceElement
    {
        private string Input;
        private static List<string> announceTypes = new List<string>(new string[] { "diamond", "club", "heart", "spade", "alltrump", "notrump" });
        public static string AnnounceUsage = "Please enter a valid announce value : [type] [value]\n" + "Possible types : " + string.Join(",", announceTypes.ToArray()) + "\nPossible values are multiple of ten between 80 and 160 or \"capot\" (160)";
        public string Type { get; private set; }
        private string valueString;
        public int Value { get; private set; }
        public bool Valid { get; private set; }
        private int CurrentValue;
        public AnnounceElement(string input, int currentValue)
        {
            Valid = true;
            Input = input.Trim();
            Value = 1;
            CurrentValue = currentValue;
        }
        public void SetValidity()
        {
            if (Input.Equals(""))
                Valid = false;
            if (Input.Count(x => x == ' ') != 1)
            {
                if (Input.Equals("pass"))
                {
                    Type = "pass";
                    Value = 0;
                    Valid = true;
                }
                else
                    Valid = false;
            }
            else
            {
                Type = Input.Split(' ').First();
                valueString = Input.Split(' ').Last();
                if (valueString.Equals("capot"))
                    Value = 160;
                else if (int.TryParse(valueString, out int tmpValue))
                {
                    Value = tmpValue;
                    Valid = true;
                    if (Value < 80 || Value > 160 || Value <= CurrentValue)
                        Valid = false;
                }
                else
                    Valid = false;
                if (!announceTypes.Contains(Type))
                    Valid = false;
                Console.WriteLine("Element is :" + Valid + " type is: " + Type + " and value is " + Value);
            }
        }
    }
}
