using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class MatchControl : SegmentControl
    {
        public Match Match = new Match();

        public MatchControl(string name, int duration) : base(SegmentType.Match, name, duration)
        {
            Match.Id = Match.GetNewId();
        }

        public MatchControl(int duration) : this("None", duration)
        {
        }

        public void AddMember(MatchMember member, int side)
        {
            // Check to ensure that the new member is being added to a side that exists.
            if(!SideExists(side))
                throw new SegmentException("Side does not exist.");
            if (MemberExists(member.Id))
                throw new SegmentException("Character is already used in this segment.");

            SideList[side - 1].AddMember(member);
            UpdateName();
        }

        public override bool VerifySegment()
        {
            // Is there more than one side?
            if (SideList.Count <= 1)
                throw new SegmentException("The match only has one side.");

            // Make sure that the base conditions are met.
            return base.VerifySegment();
        }

        public void ExportMatch()
        {
            ExportSegment(Match);
        }
    }
}
