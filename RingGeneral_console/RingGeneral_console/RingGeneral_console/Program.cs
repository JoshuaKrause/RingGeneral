using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RingGeneral_console
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager.InitializeData();
            foreach (var pair in DataManager.CharacterHandler)
                CharacterControl.InitializeCharacter(pair.Value);

            Character characterA = DataManager.CharacterHandler["CH1000"];
            Character characterB = DataManager.CharacterHandler["CH1001"];
            Character characterC = DataManager.CharacterHandler["CH1002"];

            Match matchA = new Match();

            Tag tagA = DataManager.TagHandler["TG1000"];

            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.TagHandler.Values.ToList()), "tagNew");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.CharacterHandler.Values.ToList()), "characters_new");

            ModifierControl.AddModifier(matchA, tagA);

        }
    }
}
