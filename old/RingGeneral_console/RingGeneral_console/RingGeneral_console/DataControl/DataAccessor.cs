
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Dynamic;
using System.Text;


namespace RingGeneral_console
{
    /// <summary>
    /// Stores and retrieves strings from external TXT files.
    /// </summary>
    static class DataAccessor
    {

        /// <summary>
        /// Copies the default data and sets the dataPath variable.
        /// </summary>
        /// <param name="targetFolder"></param>
        static public void CopyDataFolder(string targetPath)
        {
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            foreach (string path in Directory.GetDirectories(Globals.CURRENT_DATA, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(path.Replace(Globals.CURRENT_DATA, targetPath));

            foreach (string path in Directory.GetFiles(Globals.CURRENT_DATA, "*.*", SearchOption.AllDirectories))
                File.Copy(path, path.Replace(Globals.CURRENT_DATA, targetPath), true);
        }

        /// <summary>
        /// Saves a string to the data file.
        /// </summary>
        /// <param name="data"></param>
        static public void SaveDatabase(string data, string name)
        {
            string fileName = name + ".txt";
            string filePath = Path.Combine(Globals.CURRENT_DATA, fileName);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(data);
                sw.Close();
            }
        }
        /// <summary>
        /// Reads a string from the data file.
        /// </summary>
        /// <returns></returns>
        static public string ReadDatabase(string name)
        {
            string fileName = name + ".txt";
            string filePath = Path.Combine(Globals.CURRENT_DATA, fileName);
            string data = "";
            try
            {
                using(StreamReader sr = new StreamReader(filePath))
                {
                    data = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch(Exception)
            {
                Console.WriteLine("File import failed. Does it exist?\nPath: {0}\n\n", filePath);
            }
            return data;
        }
        /// <summary>
        /// Custom exceptions are sent to the log.
        /// </summary>
        /// <param name="input"></param>
        static public void Log(string input)
        {
            StringBuilder logString = new StringBuilder();
            logString.Append("---------------------\n");
            logString.Append(input);
            logString.Append("---------------------\n\n\n");

            using (StreamWriter sw = new StreamWriter(Globals.LOG, true))
            {
                sw.Write(logString.ToString());
                sw.Close();
            }
        }
    }
}
