using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class Segment : GameObject, IModifiable
    {
        public string SegmentName { get; set; }
        public string Description { get; set; }

        public int Duration { get; set; }

        public int OverallScore { get; set; }
        public string Results { get; set; }

        public string Modifiers { get { return StipulationModifiers; } }

        public string StipulationModifiers { get; set; }
        public SegmentModule Stipulations = new SegmentModule();
    }
}
