using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RingGeneral
{
    static partial class DataControl
    {
        const string filePath = @"C:\Users\jkrau\OneDrive\Documents\GitHub\RingGeneral_02\";
        const string loadFile = @"ringGeneralLoad.txt";
        const string saveFile = @"ringGeneralSave.txt";

        public static Dictionary<string, Character> characters;
        public static Dictionary<string, Tag> tags;

        // Loads data into a series of Dictionaries.
        public static void LoadData(string path = null)
        {
            // If no path is supplied, use the default.
            if (path == null)
                path = filePath + loadFile;

            // Initialize our data string and the file stream.
            string data;
            FileStream fileStream = null;

            // Import all data.
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                using (TextReader textReader = new StreamReader(fileStream))
                {
                    fileStream = null;
                    data = textReader.ReadToEnd();
                }
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Dispose();
            }

            // Deserialize the data.
            Deserialize(data, out characters, out tags);
        }

        public static void SaveData(string path = null)
        {
            // If no path is supplied, use the default.
            if (path == null)
                path = filePath + saveFile;

            // Serialize data by converting the dictionaries to lists of game objects.
            string data = Serialize(ConvertList(characters));
            data += Serialize(ConvertList(tags));

            // Write all data.
            try
            {
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    textWriter.Write(data);
                    textWriter.Flush();
                    textWriter.Close();
                }                  
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
