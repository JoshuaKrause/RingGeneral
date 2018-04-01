using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    abstract class Member
    {
        public Character Character;
        public string Id { get; }
        public string MemberName { get; }

        public string PrimarySkills { get; set; }
        public string SecondarySkill { get; set; }

        public int TotalSegments { get; set; }
        public float TotalScore { get; set; }

        public abstract string GetPrimarySkill();
        public abstract string ReturnSecondarySkill();

        public Member(Character character)
        {
            Character = character;
            MemberName = Character.AliasLong;
            Id = Character.Id;

            TotalSegments = 0;
            TotalScore = 0f;
        }
    }
}
