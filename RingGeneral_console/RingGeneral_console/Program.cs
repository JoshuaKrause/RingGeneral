using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RingGeneral_console
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager.InitializeData();
            foreach (var pair in DataManager.CharacterHandler)
                ModifierControl.RebuildObject(pair.Value);

            Character characterA = DataManager.CharacterHandler["CH1000"];
            Character characterB = DataManager.CharacterHandler["CH1001"];

            MatchControl match = new MatchControl(12);

            match.AddSide(2);

            match.AddMember(characterA, 1);
            match.AddMember(characterB, 2);

            //Console.WriteLine(DataManager.StipulationHandler["ST1000"]);
            //Console.WriteLine("\n");
            match.AddStipulation(DataManager.StipulationHandler["ST1000"]);

            match.RunSegment();
            match.ExportSegment(match.Match);

            Console.WriteLine(match.Match.Stipulations.ToString());

        }
    }
}
