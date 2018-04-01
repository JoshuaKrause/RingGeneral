using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    /// <summary>
    /// Takes a segment object and creates a list of characters to iterate through a match or angle.
    /// </summary>
    class MatchOrder
    {
        SegmentControl Segment;

        public MatchOrder(SegmentControl segment)
        {
            Segment = segment;
        }

        /// <summary>
        /// Assembles the match participants in equally distributed but random list of Ids.
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetMatchOrder()
        {
            if (Segment.SideList.Count == 1)
                return ShuffleSide();
            else
            {
                // Calcuate the number of phases per side.
                int minPhases = Segment.SideList.Count - 1;

                // Calculate the minimum duration for each member of each side
                // to face every other member at least once.
                int minDuration = (minPhases * Segment.SideList.Count) / 2;

                // If the match is shorter than the minimum duration, flag it.
                if (Segment.Duration < minDuration ||
                    (Segment.Type == SegmentControl.SegmentType.Match && Segment.Duration < Globals.MinimumMatchDuration) ||
                    (Segment.Type == SegmentControl.SegmentType.Angle && Segment.Duration < Globals.MinimumAngleDuration))
                    Segment.TooShort = true;

                // BaseTime is a multiple of the minimum duration.
                // OverTime is the remainder.
                int overTime = Segment.Duration % minDuration;
                int baseTime = Segment.Duration - overTime;

                // PhasesEach is the number of phases each team will have during baseTime.
                int phasesEach = minPhases * (baseTime / minDuration);

                // GetBaseMatrix returns the distribution of moves for the minimum duration.
                List<bool[]> baseMatrix = GetBaseMatrix();
                // GetExtendedMatrix extends the BaseMatrix through the duration of the match.
                List<bool[]> extendedMatrix = GetExtendedMatrix(baseMatrix);

                // Assemble match populates the ExtendedMatrix with the members of each side.
                // Then it randomizes the data and returns it as a list.
                return AssembleSegmentList(extendedMatrix);
            }
        }

        /// <summary>
        /// For one-sided angles, shuffle up the lone side and return it as a list.
        /// </summary>
        /// <returns></returns>
        List<List<String>> ShuffleSide()
        {
            // For one-sided angles, the minimum angle duration is the size of the side or the minimum, whichever is greater.
            if (Segment.Duration < Globals.MinimumAngleDuration || Segment.Duration < Segment.SideList[0].MemberList.Count)
                Segment.TooShort = true;

            // Create an empty list filled with Lists equal to the segment's duration.
            List<List<String>> matchOrder = new List<List<String>>();
            for (int time = 0; time < Segment.Duration; time++)
                matchOrder.Add(new List<string>());

            // If there's just one member, return a list containing just his or her name.
            if (Segment.SideList[0].MemberList.Count == 1)
            {
                for (int i = 0; i < Segment.Duration; i++)
                {
                    matchOrder[i].Add(Segment.SideList[0].MemberList[0].Id);
                }
                return matchOrder;                
            }
            
            // If there's more than one member, distribute them through the length of the segment.
            // Return a shuffled list.
            else
            {
                int count = 0;
                for (int i = 0; i < Segment.Duration; i++)
                {
                    if (count >= Segment.SideList[0].MemberList.Count)
                        count = 0;
                    matchOrder[i].Add(Segment.SideList[0].MemberList[count].Id);
                    count++;
                }
                matchOrder = matchOrder.OrderBy(item => Randomizer.Between(0, matchOrder.Count)).ToList();
                return matchOrder;
            }
        }

        /// <summary>
        /// Creates an array of booleans containing the combinations available for two sides of a
        /// match being active at any given time.
        /// </summary>
        /// <returns></returns>
        List<bool[]> GetBaseMatrix()
        {
            List<bool[]> colList = new List<bool[]>();
            int minPhase = Segment.SideList.Count - 1;
            for (int primary = 0; primary <= minPhase; primary++)
            {
                for (int secondary = 0; secondary < Segment.SideList.Count; secondary++)
                {
                    if (primary != secondary)
                    {
                        bool[] boolArray = new bool[Segment.SideList.Count];
                        boolArray[primary] = true;
                        boolArray[secondary] = true;
                        bool match = false;
                        foreach (bool[] storedArray in colList)
                        {
                            if (storedArray.SequenceEqual(boolArray))
                                match = true;
                        }
                        if (!match)
                            colList.Add(boolArray);
                    }
                }
            }
            return colList;
        }

        /// <summary>
        /// Takes a baseMatrix and extends it through the duration of the match.
        /// </summary>
        /// <param name="baseMatrix"></param>
        /// <returns></returns>
        List<bool[]> GetExtendedMatrix(List<bool[]> baseMatrix)
        {
            List<bool[]> extendedMatrix = new List<bool[]>();

            // Iterate through the baseMatrix and copy its contents to the extendedMatrix.
            // When the end of the baseMatrix is reached, restart at 0.
            int baseIndex = 0;
            for (int time = 0; time < Segment.Duration; time++)
            {
                if (baseIndex >= baseMatrix.Count)
                    baseIndex = 0;
                extendedMatrix.Add(baseMatrix[baseIndex]);
                baseIndex++;
            }
            return extendedMatrix;
        }


        /// <summary>
        /// Takes the ExtendedMatrix and populates it with the workers in the match.
        /// </summary>
        /// <param name="extendedMatrix"></param>
        /// <returns></returns>
        List<List<string>> AssembleSegmentList(List<bool[]> extendedMatrix)
        {
            // Create an empty <List<List<string>> and fill it with a
            // number of empty lists equal to the duration of the match.
            List<List<string>> matchOrder = new List<List<string>>();

            for (int time = 0; time < Segment.Duration; time++)
                matchOrder.Add(new List<string>());

            // Create a list of lists containing an even distribution the workers for each side of the match.
            // Then randomize it.
            List<List<string>> workerOrderList = new List<List<string>>();
            for (int side = 0; side < Segment.SideList.Count; side++)
            {
                List<string> workerOrderSubList = new List<string>();
                int worker = 0;
                foreach (bool[] minute in extendedMatrix)
                {
                    if (worker == Segment.SideList[side].MemberList.Count)
                        worker = 0;
                    if (minute[side])
                        workerOrderSubList.Add(Segment.SideList[side].MemberList[worker].Id);
                    worker++;
                }
                workerOrderSubList = workerOrderSubList.OrderBy(item => Randomizer.Between(0, workerOrderSubList.Count)).ToList();
                workerOrderList.Add(workerOrderSubList);
            }

            // Iterate through the matrix. If a side is present in a minute, pull a worker from their list.
            // Delete the used entry. 
            for (int side = 0; side < Segment.SideList.Count; side++)
            {
                for (int time = 0; time < Segment.Duration; time++)
                {
                    if (extendedMatrix[time][side])
                    {
                        matchOrder[time].Add(workerOrderList[side][0]);
                        workerOrderList[side].RemoveAt(0);
                    }
                }
            }
            // Randomize the list and return it.
            matchOrder = matchOrder.OrderBy(item => Randomizer.Between(0, matchOrder.Count)).ToList();
            return matchOrder;
        }
    }
}
