using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RingGeneral_console
{    
    static class DataSerializer
    {
        // Splits a string of formatted text into a Dictionary of IDs and objects.
        static public Dictionary<string, T> Deserialize<T>(string input) 
            where T : GameObject
        {
            Dictionary<string, T> output = new Dictionary<string, T>();
            Dictionary<string, List<string[]>> splitEntry = SplitEntry(input);

            foreach (var pair in splitEntry)
            {
                T returnedClass = AssembleClass<T>(pair.Key, pair.Value);
                output.Add(pair.Key, returnedClass);
            }
            return output;
        }
        //static public Dictionary<string, T> Deserialize<T>(Dictionary<string, List<string[]>> input) 
        //    where T : GameObject
        //{
        //    Dictionary<string, T> output = new Dictionary<string, T>();
        //    foreach (var pair in input)
        //    {
        //        T returnedClass = AssembleClass<T>(pair.Key, pair.Value);
        //        output.Add(pair.Key, returnedClass);
        //    }
        //    return output;
        //}    
        // Saves a class as a string.
        static public string Serialize<T>(List<T> objectList)
            where T : GameObject
        {
            StringBuilder output = new StringBuilder();
            foreach (var gameObject in objectList)
            {
                output.Append(gameObject.ToString());
                if (gameObject.Equals(objectList.Last<T>()))
                    break;
                else
                {
                    output.Append("}\n");
                }                   
            }          
            return output.ToString();
        }
        // Splits a text file into a Dictionary containing the ID and a list of lines.
        static private Dictionary<string, List<string[]>> SplitEntry(string input)
        {
            Dictionary<string, List<string[]>> classDict = new Dictionary<string, List<string[]>>();
            // Split the entries and remove any blanks.
            List<string> tempEntries = input.Split('}').ToList();
            for(int i = tempEntries.Count - 1; i >= 0; i--)
            {
                if (tempEntries[i].Length < 1)
                    tempEntries.RemoveAt(i);
            }

            // Split the entries into lines and store the first line as the class Id.
            foreach (string entry in tempEntries)
            {
                List<string> tempLines = entry.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                string classId = tempLines[0].Trim();
                tempLines.RemoveAt(0);

                // Split the lines into arrays. Clean the string and store them in the output array.
                // Then store the array in the output list.
                List<string[]> classLines = new List<string[]>();
                foreach(string line in tempLines)
                {
                    string[] tempArray = line.Split('=');
                    string[] classArray = new string[tempArray.Length];
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        classArray[i] = tempArray[i].Trim().TrimEnd(';').Trim();
                    }

                    classLines.Add(classArray);
                }
                // Add the ClassId and the classLines to our output Dictionary.
                classDict.Add(classId, classLines);
            }
            return classDict;
        }

        // Converts a List of lines into a class.
        static private T AssembleClass<T>(string id, List<string[]> input) 
            where T : GameObject
        {
            // Get the type of the class and create an instance of it with an id.
            Type type = typeof(T);
            T temp = Activator.CreateInstance<T>();
            temp.Id = id;

            // Iterate through the lines for properties and values. 
            // Some value types require additional casting.
            foreach (string[] item in input)
            {
                Type convertedType = type.GetProperty(item[0]).PropertyType;

                dynamic newValue;

                newValue = Convert.ChangeType(item[1], convertedType);
                type.GetProperty(item[0]).SetValue(temp, newValue);
            }

            // Update the class and return it.
            return temp;
        }

        // Converts a text entry into a spreadsheet separated by tabs.
        static public string ConvertToCSV(string input)
        {
            StringBuilder output = new StringBuilder();

            // Split our string into a Dictionary of string arrays.
            Dictionary<string, List<string[]>> objectDict = SplitEntry(input);
            
            // The first row is the listing of fields.
            List<string> fields = new List<string>();
            string key = objectDict.Keys.ToArray()[0];
            fields.Add("Id");
            for (int i = 0; i <= objectDict[key].Count - 1; i++)
            {
                fields.Add(objectDict[key][i][0]);
            }
            output.Append(string.Join("\t", fields));
            output.Append("\n");

            // The remaining fields are the values.
            // Iterate through them and add tabs.
            foreach (var pair in objectDict)
            {
                List<string> values = new List<string>();
                values.Add(pair.Key);
                foreach(var line in pair.Value)
                {
                    values.Add(line[1]);
                }
                output.Append(string.Join("\t", values));
                if(pair.Value != objectDict.Values.Last())
                    output.Append("\n");
            }
            return output.ToString();
        }

        // Converts a spreadsheet separated by tabs into a Dictionary of properties which can be assembled into a class.
        static public Dictionary<string, List<string[]>> ConvertFromCSV(string input)
        {
            Dictionary<string, List<string[]>> outputDict = new Dictionary<string, List<string[]>>();

            // Break the string into lines.
            //List<string> tempObject = input.Split('\n').ToList<string>();
            List<string> tempObject = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            // Store the keys and remove them from the main list.
            List<string> keys = tempObject[0].Split('\t').ToList<string>();
            tempObject.RemoveAt(0);
            keys.RemoveAt(0);
            
            // Iterate through the remaining objects.
            foreach (string obj in tempObject)
            {
                // Separate the string by removing the tabs.
                List<string> propertyList = obj.Split('\t').ToList<string>();

                // Set the Id aside and remove it from the list.
                string id = propertyList[0].Trim();
                propertyList.RemoveAt(0);

                // Iterate through the remaining lines and convert to string arrays.
                List<string[]> propertiesOut = new List<string[]>();
                for (int i = 0; i < propertyList.Count; i++)
                {
                    string[] propertyArray = new string[] { keys[i], propertyList[i] };
                    propertiesOut.Add(propertyArray);
                }
                // Add the Id and the assembled list of string arrays.
                outputDict.Add(id, propertiesOut);             
            }            
            return outputDict;
        }
    }
}
