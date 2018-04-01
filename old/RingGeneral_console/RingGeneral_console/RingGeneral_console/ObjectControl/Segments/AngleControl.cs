using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class AngleControl : SegmentControl
    {

        public Angle Angle = new Angle();

        public AngleControl(string name, int duration) : base(SegmentType.Angle, name, duration)
        {
            Angle.Id = Angle.GetNewId();
        }

        public AngleControl(int duration) : this("None", duration)
        {
        }

        public void AddMember(AngleMember member, int side)
        {
            // Check to ensure that the new member is being added to a side that exists.
            if (!SideExists(side))
                throw new SegmentException("Side does not exist.");
            if (MemberExists(member.Id))
                throw new SegmentException("Character is already used in this segment.");

            SideList[side - 1].AddMember(member);
            UpdateName();
        }

        public override bool VerifySegment()
        {
            // Is there at least one side?
            if (SideList.Count <= 0)
                throw new SegmentException("The angle has no sides.");

            // Is there at least one active participant?
            // Iterate through all members and mark those not absent or neutral.
            int active = 0;
            foreach (Side side in SideList)
            {
                foreach (AngleMember angleMember in side.MemberList)
                {
                    if (!angleMember.Absent && !angleMember.Neutral)
                        active++;
                }
            }
            if (active <= 0)
                throw new SegmentException("The angle has no active participants.");

            return base.VerifySegment();
        }
    }
}
