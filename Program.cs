using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    class Program
    {
        static void Main(string[] args)
        {
            DataControl.LoadData();
            Character hogan = DataControl.characters["CH1000"];
            Console.WriteLine(hogan);
            Tag t2 = DataControl.tags["TG1001"];
            Console.WriteLine(t2);
            DataControl.SaveData();

        }
    }
}
