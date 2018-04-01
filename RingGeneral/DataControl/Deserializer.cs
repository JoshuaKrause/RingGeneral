using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RingGeneral
{
    static partial class DataControl
    {
        // Takes a string and breaks it down into dictionaries, which are then used to create dictionaries of GameObjects (characters, tags, etc).
        public static Dictionary<string, T> Deserialize<T>(string data)
            where T : GameObject
        {
            Dictionary<string, T> output = new Dictionary<string, T>();

            // Remove break characters from the raw data. Split it into sub-lists.
            data = CleanData(data);

            List<string> dataList = SplitData(data, '}');

            // Assemble object dictionaries.
            return CreateObjectDict<T>(dataList);
        }

        // Removes break characters from strings.
        public static string CleanData(string data)
        {
            return Regex.Replace(data, @"\t|\n|\r", string.Empty);
        }

        // Splits data string, removes blank entries, and returns a list.
        static List<string> SplitData(string input, char stripee)
        {
            var output = input.Split(stripee).ToList();
            output = output.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            return output;
        }

        // Returns a dictionary created from the keys and values created by the split string.
        static Dictionary<string, string> CreatePropertyDict(string input)
        {
            List<string> rawProperties = SplitData(input, ';');
            Dictionary<string, string> propertyDict = new Dictionary<string, string>();
            foreach (var property in rawProperties)
            {
                propertyDict.Add(property.Split('=')[0], property.Split('=')[1]);
            }
            return propertyDict;
        }

        // Returns a dictionary of objects created from the initial list of strings.
        static Dictionary<string, T> CreateObjectDict<T>(List<string> data)
            where T : GameObject
        {
            Dictionary<string, T> output = new Dictionary<string, T>();
            foreach (var item in data)
            {
                Dictionary<string, string> propertiesDict = CreatePropertyDict(item);
                T newObject = CreateObject<T>(propertiesDict);
                output.Add(newObject.Id, newObject);
            }
            return output;
        }

        // Returns an object from the supplied dictionary.
        static T CreateObject<T>(Dictionary<string, string> input)
            where T : GameObject
        {
            Type type = typeof(T);
            T output = (T)Activator.CreateInstance(typeof(T), input);
            output.Id = input["Id"];
            return output;
        }
    }
}
