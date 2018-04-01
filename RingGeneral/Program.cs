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
            List<Tuple<int, Conflict.Result>> resultList = new List<Tuple<int, Conflict.Result>>();
            int skill = 50;

            for (int i = 0; i < 5; i++)
                resultList.Add(Conflict.SkillRoll(skill));

            foreach (var result in resultList)
                Console.WriteLine(result);

            Console.WriteLine(string.Join(",",Conflict.GetThresholds(skill)));
        }
    }
}
