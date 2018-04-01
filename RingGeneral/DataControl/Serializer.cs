using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    static partial class DataControl
    {
        // Iterates through a dynamic list of GameObjects and returns their contents as an exportable string.
        public static string Serialize(List<IExportable> data)
        {
            StringBuilder output = new StringBuilder();

            foreach (IExportable entry in data)
            {
                try
                {
                    output.AppendFormat("{0}{1}\n", entry.Export(), '}');
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }                    
            }
            //output.Append("{\n");
            return output.ToString();
        }

        // Converts a list of objects to GameObjects.
        public static List<IExportable> ConvertList<T>(Dictionary<string, T> list)
        {
            return list.Values.Cast<IExportable>().ToList();
        }
    }
}
