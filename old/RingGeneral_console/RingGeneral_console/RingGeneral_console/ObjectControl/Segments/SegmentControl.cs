using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class SegmentControl
    {
        // Contains the segment's side(s).
        public List<Side> SideList = new List<Side>();

        // The segment's name and a check to see if it was generated or supplied.
        public string SegmentName { get; set; }
        bool ManualName;

        // The match type: Match or Angle
        public enum SegmentType { None, Match, Angle };
        public SegmentType Type { get; }

        // The length of the match or angle.
        public int Duration { get; set; }

        // A description of the segment.
        string Description { get; set; }

        // The match order.
        public List<List<Member>> MatchOrder;

        // The match score.
        public int OverallScore;
        public string Results;

        // Check to see if the segment is too short.
        public SegmentModule Stipulations = new SegmentModule();
        public bool TooShort;

        bool segmentRun;

        public SegmentControl(SegmentType type, string name, int duration)
        {
            Type = type;
            Duration = duration;

            if (name != "None")
                ManualName = true;
            else
                SegmentName = name;
        }

        public SegmentControl(SegmentType type) : this(type, "None", 5)
        {
        }

        public SegmentControl() : this(SegmentType.Match, "None", 5)
        {
        }

        // Adds an empty Side object to the SideList.
        public void AddSide()
        {
            SideList.Add(new Side());
        }

        // Adds multiple empty sides to the SideList.
        public void AddSide(int sides)
        {
            for (int i = 0; i < sides; i++)
                AddSide();
        }

        // Check to ensure that a side exists before adding a new member.
        protected bool SideExists(int side)
        {
            if (side > SideList.Count)
                return false;
            else
                return true;
        }

        // Checks for empty sides.
        protected bool SideFull()
        {
            foreach (Side side in SideList)
            {
                if (side.MemberList.Count <= 0)
                    return false;
            }
            return true;
        }

        // Check to see if a character has already been added to a side.
        protected bool MemberExists(string id)
        {
            foreach (Side side in SideList)
            {
                foreach (Member member in side.MemberList)
                {
                    if (id.Equals(member.Id))
                        return true;
                }
            }
            return false;
        }

        // Refreshes the SegmentName property.
        public void UpdateName()
        {
            if (!ManualName)
                SegmentName = GetName();
        }

        // Generate a name for the segment.
        public virtual string GetName()
        {
            StringBuilder name = new StringBuilder();
            for (int side = 0; side < SideList.Count; side++)
            {
                name.Append(SideList[side].SideName);
                if (side != SideList.Count - 1)
                    name.Append(" vs. ");
            }
            return name.ToString();
        }

        /// Coverts the MatchOrder from a list of strings to a list of MatchMembers.
        /// </summary>
        public void GetMatchOrder()
        {
            if (VerifySegment())
            {
                List<List<string>> OrderList = new MatchOrder(this).GetMatchOrder();
                Dictionary<string, Member> MemberDict = new Dictionary<string, Member>();

                foreach (Side side in SideList)
                {
                    foreach (Member member in side.MemberList)
                    {
                        MemberDict.Add(member.Id, member);
                    }
                }

                List<List<Member>> matchList = new List<List<Member>>();
                foreach (List<string> round in OrderList)
                {
                    List<Member> roundList = new List<Member>();
                    foreach (string member in round)
                    {
                        //Console.WriteLine(roundList.Count);
                        roundList.Add(MemberDict[member]);
                    }
                    matchList.Add(roundList);
                }
                MatchOrder = matchList;
            }
        }

        // Safety check to ensure that the segment is valid.
        public virtual bool VerifySegment()
        {
            // Is the duration a postive number?
            if (Duration < 1)
                throw new SegmentException("The segment duration is less than one minute.");
            // Is there at least one worker per side?
            if (!SideFull())
                throw new SegmentException("One or more sides are empty.");
            return true;
        }

        public void SetWinner(int side)
        {
            SideExists(side);
            SideList[side - 1].Winner = true;
        }

        public void SetLoser(int side)
        {
            SideExists(side);
            SideList[side - 1].Winner = false;
        }

        public override string ToString()
        {
            return SegmentName;
        }

        public void RunSegment()
        {
            if (segmentRun)
                throw new SegmentException("This segment has already been run.");
            if (MatchOrder == null)
                throw new SegmentException("The match order hasn't been determined.");

            StringBuilder resultString = new StringBuilder();

            // Iterate through the matchOrder list and submit each matchUp for conflict resolution.
            float matchScoreTotal = 0f;
            int time = 1;
            foreach (List<Member> round in MatchOrder)
            {
                float roundScoreTotal = 0f;
                resultString.AppendFormat("\n------------------------\nMinute {0}:\n", time);
                foreach (Member member in round)
                {
                    string primarySkill = member.GetPrimarySkill();
                    int skillValue = (int)typeof(SkillModule).GetProperty(primarySkill).GetValue(member.Character.Skills, null);
                    Conflict.Result result = Conflict.ConflictRoll(skillValue, member.Character.Skills.Instinct);

                    // Pass the result and the skill used to CalculateScore to get a value.
                    int finalResult = Conflict.CalculateScore(skillValue, result);

                    // Update personal information in the character's MatchSkills entry.
                    member.TotalSegments += 1;
                    member.TotalScore += finalResult;

                    // Update the roundScoreTotal.
                    roundScoreTotal += finalResult;

                    resultString.AppendFormat("  {0} fought with {1} and rolled a {2}. Score: {3}\n",
                        member.Character.AliasShort,
                        primarySkill,
                        result.ToString(),
                        finalResult);
                }
                // Average the roundScoreTotal and apply it to the matchScoreTotal.
                matchScoreTotal += roundScoreTotal / 2;
                resultString.AppendFormat("Round total: {0}", roundScoreTotal / 2);
                time++;
            }

            // Divide the matchScoreTotal by the number of rounds. Store the final total to the Match object.
            int matchScore = Convert.ToInt16(matchScoreTotal / Duration);
            OverallScore = matchScore;

            resultString.AppendFormat("\n\n-----------------\nFINAL MATCH SCORE: {0}", matchScore);
            Results = resultString.ToString();
            segmentRun = true;
        }

        public void ExportSegment(Segment segment)
        {
            if (!segmentRun)
                throw new SegmentException("Segment hasn't run yet. Unable to export.");
            segment.SegmentName = SegmentName;
            segment.Results = Results;
        }
    }
}
