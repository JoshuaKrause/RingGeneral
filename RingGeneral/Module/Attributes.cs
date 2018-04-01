using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    public class Attributes
    {

        public Dictionary<string, int> AttributeDict;
        
        public static List<string> keyList = new List<string>() { "Brawling",
                                                                  "Grappling",
                                                                  "Flying",
                                                                  "Power",
                                                                  "Instinct",
                                                                  "Presence",
                                                                  "Flair",
                                                                  "Fit_Flabby",
                                                                  "Tough_Frail",
                                                                  "Aggressive_Calm",
                                                                  "Trusting_Suspicious",
                                                                  "Leader_Follower",
                                                                  "Selfish_Generous",
                                                                  "Loyal_Disloyal",
                                                                  "Reckless_Conservative",
                                                                  "Creative_Dull" };

        public Attributes()
        {
            AttributeDict = new Dictionary<string, int>();

            foreach (string key in keyList)
                AttributeDict.Add(key, 0);
        }

        public void AddAttributes(Attributes modifier)
        {
            foreach (KeyValuePair<string, int> entry in modifier.AttributeDict)
                AttributeDict[entry.Key] += modifier.AttributeDict[entry.Key];
        }

        public void SubtractAttributes(Attributes modifier)
        {
            foreach (KeyValuePair<string, int> entry in modifier.AttributeDict)
                AttributeDict[entry.Key] -= modifier.AttributeDict[entry.Key];
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            foreach (KeyValuePair<string, int> entry in AttributeDict)
                output.AppendFormat("{0}={1};\n", entry.Key, entry.Value);
            
            return output.ToString();
        }
    }
}

