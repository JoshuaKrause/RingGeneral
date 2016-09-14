using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class AngleMember : Member
    {

        public bool Neutral;
        public bool Absent;

        public AngleMember(Character character, string primarySkills, string secondarySkill, bool neutral = false, bool absent = false) : base(character)
        {

            PrimarySkills = primarySkills;
            SecondarySkill = secondarySkill;
            Neutral = neutral;
            Absent = absent;

            TotalScore = 0f;
        }

        public override string GetPrimarySkill()
        {
            return PrimarySkills;
        }

        public override string ReturnSecondarySkill()
        {
            return SecondarySkill;
        }
    }
}
