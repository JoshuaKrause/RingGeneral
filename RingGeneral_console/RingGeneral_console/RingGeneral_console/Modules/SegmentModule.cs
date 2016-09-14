using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class SegmentModule : Module
    {
        public bool Traditional { get; set; }    // Traditional wrestling. No gimmicks.
        public bool Gimmick { get; set; }        // Cage matches, lumberjack matches. Referees are still involved.
        public bool Hardcore { get; set; }       // Weapons matches. No disqualification matches. No referees involved.

        public bool Singles { get; set; }
        public bool TagTeams { get; set; }

        public bool Blood { get; set; }

        public bool WomenOnly { get; set; }
        public bool InterGender { get; set; }

        public bool Hosses { get; set; }         // Matches featuring super-heavyweights or giants.
        public bool Cruisers { get; set; }       // Matches featuring cruiserweights or minis.

        public bool Brawlers { get; set; }
        public bool Grapplers { get; set; }
        public bool Flyers { get; set; }
        public bool Powerhouses { get; set; }

        public bool LongMatches { get; set; }
        public bool ShortMatches { get; set; }
    }
}
