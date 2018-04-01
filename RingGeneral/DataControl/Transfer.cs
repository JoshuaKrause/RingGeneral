using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RingGeneral
{
    partial class DataControl
    {
        // Converts formatted data into a comma-separated-values spreadsheet.
        static string ConvertToCSV(string data)
        {
            StringBuilder output = new StringBuilder();

            data = CleanData(data);
            List<string> dataList = SplitData(data, '}');

            // The first row should be column headers.
            output.AppendLine(string.Join(";", CreatePropertyDict(dataList[0]).Select(o => o.Key).ToList()));

            // Iterate through the remaining lists of values and join them with commans.
            foreach (string entry in dataList)
                output.AppendLine(string.Join(";", CreatePropertyDict(entry).Select(o => o.Value).ToList()));

            return output.ToString();
        }

        static string CSVToData<T>(string data)
            where T : GameObject, IExportable
        {
            StringBuilder output = new StringBuilder();

            data = Regex.Replace(data, @"\t|\r", string.Empty);

            List<string> dataList = SplitData(data, '\n');
            List<string> keys = SplitData(dataList[0], ';');
            dataList.RemoveAt(0);

            List<T> objectList = new List<T>();

            for (int i = 0; i < dataList.Count; i++)
            {
                Dictionary<string, string> propertyDict = new Dictionary<string, string>();
                List<string> values = SplitData(dataList[i], ';');
                for (int j = 0; j < values.Count; j++)
                {
                    propertyDict.Add(keys[j], values[j]);
                }

                T newObject = CreateObject<T>(propertyDict);
                objectList.Add(newObject);
            }
            foreach (var gameObject in objectList)
            {
                output.Append(gameObject.Export());
                output.AppendLine("}");
            }

            return output.ToString();                         
        }
    }
}
