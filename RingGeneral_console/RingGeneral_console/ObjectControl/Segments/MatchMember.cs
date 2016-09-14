using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class MatchMember : Member
    {
        public MatchMember(Character character) : base(character)
        {
            PrimarySkills = GetMatchSkills();
            SecondarySkill = "Instinct";
        }

        /// <summary>
        ///  Determines the order of RingSkills from best to worst.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        public string GetMatchSkills()
        {
            // Create a list of in-ring skills and organize it from best to worst.
            List<string> propertyList = new List<string>() { "Brawling", "Grappling", "Flying", "Power" };

            var orderedList = from property in propertyList
                              orderby typeof(SkillModule).GetProperty(property).GetValue(Character.Skills, null) descending
                              select property;

            return string.Join(",", orderedList);
        }

        public override string GetPrimarySkill()
        {
            return ReturnRandomSkill();
        }

        public override string ReturnSecondarySkill()
        {
            return SecondarySkill;
        }

        /// <summary>
        /// Returns the RingSkills string as a list.
        /// </summary>
        /// <returns></returns>
        List<string> GetPrimarySkillList()
        {
            return Regex.Split(PrimarySkills, ",").ToList();
        }

        // Returns a random skill from the RingSkills string.
        string ReturnRandomSkill()
        {
            List<string> PrimarySkillList = GetPrimarySkillList();
            int count = PrimarySkillList.Count;

            int[] skillArray = new int[count];
            int[] skillChance = new int[count];

            // Collect the skill values and store their sum.
            for (int i = 0; i < count; i++)
            {
                skillArray[i] += (int)typeof(SkillModule).GetProperty(PrimarySkillList[i]).GetValue(DataManager.CharacterHandler[Id].Skills, null);
            }
            int sum = skillArray.Sum();

            // Find the percentage chance of each skill being used and subtract it from the remaining percent.
            // If a result comes back as zero, bump it up to 1.
            int percent = 100;
            for (int i = 0; i < count; i++)
            {
                skillChance[i] += Convert.ToInt16(Math.Round((Convert.ToDecimal(skillArray[i]) / sum) * 100));
                percent = percent - skillChance[i];
                if (percent == 0)
                    percent = 1;
                skillChance[i] = percent;
            }

            // Ensure that the remaining digits to the left are greater than the digits to their right.
            for (int i = count - 2; i >= 0; i--)
            {
                if (skillChance[i] <= skillChance[i + 1])
                    skillChance[i] = skillChance[i + 1] + 1;
            }

            // Roll and return the designated skill.
            int roll = Randomizer.Hundred();

            if (roll >= skillChance[0])
                return PrimarySkillList[0];
            if (roll < skillChance[0] && roll >= skillChance[1])
                return PrimarySkillList[1];
            if (roll < skillChance[1] && roll >= skillChance[2])
                return PrimarySkillList[2];
            else { return PrimarySkillList[3]; }
        }
    }
}
