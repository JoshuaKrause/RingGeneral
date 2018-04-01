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
        //const string filePath = @"C:\Users\jkrau\OneDrive\Documents\GitHub\RingGeneral_02\";
        public const string filePath = @"C:\Users\VFX_07_2\Documents\Visual Studio 2015\Projects\RingGeneral_02\Data\";
        public const string loadFile = @"DataLoad.txt";
        const string saveFile = @"DataSave.txt";

        public const string csv = @"ExportCSV.txt";

        const string tagFile = @"Tags.txt";

        public static Dictionary<string, Character> characterDict;
        public static Dictionary<string, Tag> tagDict;

        public static void InitializeData()
        {
            characterDict = LoadData<Character>(filePath + loadFile);
            tagDict = LoadData<Tag>(filePath + tagFile);
        }

        // Loads data into a series of Dictionaries.
        static Dictionary<string, T> LoadData<T>(string path)
            where T : GameObject
        {
            string data = ReadData(path);
            return Deserialize<T>(data);
        }

        public static void SaveData() { SaveData(filePath + saveFile); }

        public static void SaveData(string path)
        {
            string data = Serialize(ConvertList(characterDict));
            SaveData(path, data);
        }

        public static void SaveData(string path, string data)
        {
            WriteData(data, path);
        }


        // Export saved data to a CSV file.
        public static void ExportCSV(string importPath, string exportPath)
        {
            string data = ReadData(importPath);
            data = ConvertToCSV(data);
            WriteData(data, exportPath);
        }

        public static void TransferCSV(string importPath, string exportPath)
        {
            string data = ReadData(importPath);
            string x = CSVToData<Character>(data);
            Console.WriteLine(x);
        }

        static void WriteData(string data, string path)
        {
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

        static string ReadData(string path)
        {
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

            return data;
        }

    }
}
