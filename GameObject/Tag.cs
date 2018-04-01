using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    class Tag : GameObject
    {
        public string Name { get; set; }

        // Physical skills.
        public int Brawling { get; set; }
        public int Grappling { get; set; }
        public int Flying { get; set; }
        public int Power { get; set; }

        // Mental skills.
        public int Instinct { get; set; }
        public int Presence { get; set; }
        public int Flair { get; set; }

        // Traits.
        public int Fit_Flabby { get; set; }
        public int Tough_Frail { get; set; }
        public int Aggressive_Calm { get; set; }
        public int Trusting_Suspicious { get; set; }
        public int Leader_Follower { get; set; }
        public int Selfish_Generous { get; set; }
        public int Loyal_Disloyal { get; set; }
        public int Reckless_Conservative { get; set; }
        public int Creative_Dull { get; set; }

        List<string> attributeList = new List<string>() { "Brawling", "Grappling", "Flying", "Power",
                                                         "Instinct", "Presence", "Flair",
                                                         "Fit_Flabby", "Tough_Frail", "Aggressive_Calm",
                                                         "Trusting_Suspicious", "Leader_Follower", "Selfish_Generous",
                                                         "Loyal_Disloyal", "Reckless_Conservative", "Creative_Dull" };


        public Tag(Dictionary<string, string> propertyDict = null)
        {
            if (propertyDict != null)
            {
                Id = propertyDict["Id"];
                Name = propertyDict["Name"];

                foreach (var attribute in attributeList)
                {
                    if (propertyDict.ContainsKey(attribute))
                        GetType().GetProperty(attribute).SetValue(this, Int32.Parse(propertyDict[attribute]));
                }
            }
        }

        public override string Export()
        {
            StringBuilder output = new StringBuilder();

            output.AppendFormat("{0}={1};\n", "Id", Id);
            output.AppendFormat("\t{0}={1};\n", "Name", Name);

            foreach (string attribute in attributeList)
            {
                int attributeValue = (int)GetType().GetProperty(attribute).GetValue(this);
                if (attributeValue != 0)
                    output.AppendFormat("\t{0}={1};\n", attribute, attributeValue);
            }

            return output.ToString();
        }

        public override string ToString()
        {
            return Export();
        }
    }
}
