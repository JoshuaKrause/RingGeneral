using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class Conflict
    {
        // Conflict enums.
        public enum Result { Pop, TotalSuccess, PartialSuccess, Botch };

        /// <summary>
        /// Returns the result of a skill roll after finding the appropriate botch and pop windows.
        /// </summary>
        /// <param name="inputSkill"></param>
        /// <param name="modifierSkill"></param>
        /// <returns></returns>
        static public Result ConflictRoll(int inputSkill, int modifierSkill)
        {
            decimal average = Convert.ToDecimal(inputSkill + modifierSkill) / 2;
            int popCeiling = Convert.ToInt32(FindPop(average));
            int botchFloor = Convert.ToInt32(FindBotch(average));

            int roll = Randomizer.Between(1, 100);

            if (roll >= botchFloor)
                return Result.Botch;
            if (roll < botchFloor && roll >= inputSkill)
                return Result.PartialSuccess;
            if (roll > popCeiling && roll < inputSkill)
                return Result.TotalSuccess;
            else { return Result.Pop; }
        }

        /// <summary>
        /// ConflictRoll overload for conflicts without modifier skills.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public Result ConflictRoll(int input)
        {
            return ConflictRoll(input, input);
        }

        /// <summary>
        /// Returns the score on a conflict roll, which is the skill used modified by the Result modifier.
        /// </summary>
        /// <param name="skillValue"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        static public int CalculateScore(int skillValue, Conflict.Result result)
        {
            if (result.Equals(Conflict.Result.Pop))
                return Convert.ToInt16(skillValue * Globals.PopModifer);
            if (result.Equals(Conflict.Result.TotalSuccess))
                return Convert.ToInt16(skillValue * Globals.TotalSuccessModifier);
            if (result.Equals(Conflict.Result.PartialSuccess))
                return Convert.ToInt16(skillValue * Globals.PartialSuccessModifier);
            else
            {
                return Convert.ToInt16(skillValue * Globals.BotchModifier);
            }
        }

        /// <summary>
        /// Returns the botch floor for the supplied input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static decimal FindBotch(decimal input)
        {
            return Math.Round(100 - ((100 - input) / Globals.Swing), MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Returns the pop ceiling for the supplied input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static decimal FindPop(decimal input)
        {
            return Math.Round(input / Globals.Swing, MidpointRounding.AwayFromZero);
        }
    }
}
