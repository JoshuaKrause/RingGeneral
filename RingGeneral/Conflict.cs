using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    static class Conflict
    {
        public enum Result { Fumble, Bad, Failure, Success, Good, Critical };

        public static int[] GetThresholds(int skill)
        {           
            int fumbleRange = RoundNumber(100 - ((100 - (decimal)skill) / 20));
            int badRange = RoundNumber(100 - ((100 - (decimal)skill) / 5));
            int goodRange = RoundNumber((decimal)skill / 5);
            int criticalRange = RoundNumber((decimal)skill / 20);

            if (fumbleRange > 100)
                fumbleRange = 100;
            if (badRange > 100)
                badRange = 100;
            if (goodRange < 1)
                goodRange = 1;
            if (criticalRange < 1)
                criticalRange = 1;

            return new int[] { fumbleRange, badRange, skill, goodRange, criticalRange };
        }

        public static Tuple<int, Result> SkillRoll(int skill)
        {
            int[] thresholds = GetThresholds(skill);
            int roll = Randomizer.Hundred();

            Tuple<int, Result> result;

            if (roll >= thresholds[0])
                result = new Tuple<int, Result>(roll, Result.Fumble);
            else if (roll >= thresholds[1] && roll < thresholds[0])
                result = new Tuple<int, Result>(roll, Result.Bad);
            else if (roll > thresholds[2] && roll < thresholds[1])
                result = new Tuple<int, Result>(roll, Result.Failure);
            else if (roll <= thresholds[2] && roll > thresholds[3])
                result = new Tuple<int, Result>(roll, Result.Success);
            else if (roll <= thresholds[3] && roll > thresholds[4])
                result = new Tuple<int, Result>(roll, Result.Good);
            else
                result = new Tuple<int, Result>(roll, Result.Critical);

            return result;
        }

        public static int RoundNumber(decimal input) { return Convert.ToInt32(Math.Round(input, MidpointRounding.AwayFromZero)); }
    }
}
