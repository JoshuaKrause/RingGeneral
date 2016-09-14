using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    public class GameObject
    {
        public string Id { get; set; }

        public string GetNewId()
        {
            // If the object already has an Id, throw an exception.
            if (Id != null)
                throw new GameObjectException("Object already has a unique Id.");

            string prefix = "None";
            List<string> idList = new List<string>();

            // Find the type and get the assigned prefix.
            // This currently seems unwieldly.
            if (GetType() == typeof(Character))
            {
                prefix = Globals.CharacterPrefix;
                idList = DataManager.CharacterHandler.Keys.ToList();
            }
            if (GetType() == typeof(Match))
            {
                prefix = Globals.MatchPrefix;
                idList = DataManager.MatchHandler.Keys.ToList();
            }
            if (GetType() == typeof(Angle))
            {
                prefix = Globals.AnglePrefix;
                idList = DataManager.AngleHandler.Keys.ToList();
            }
            if (GetType() == typeof(Stipulation))
            {
                prefix = Globals.StipulationPrefix;
                idList = DataManager.StipulationHandler.Keys.ToList();
            }

            // If the list is empty, start at the initial number (currently 1000).
            if (idList.Count == 0)
            {
                return prefix + Globals.InitialId;
            }
            // Otherwise find the last entry and add the next number.
            else
            {
                List<int> idNumberList = new List<int>();
                foreach (string id in idList)
                {
                    int number = Convert.ToInt32(id.Substring(2));
                    idNumberList.Add(number);
                }

                idNumberList.Sort();

                return prefix + (idNumberList.Last() + 1);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            var properties = GetType().GetProperties();

            output.Append(string.Format("{0}\r\n", Id));
            for (int i = 0; i < properties.Length; i++)
            {
                string property = properties[i].ToString().Split(' ')[1];
                var propertyValue = properties[i].GetValue(this);
                if (property == "Id" || property == "Modifiers")
                    continue;
                else { output.Append(string.Format("{0}={1};\r\n", property, propertyValue)); }  
            }
            return output.ToString();
        }

    }
}
