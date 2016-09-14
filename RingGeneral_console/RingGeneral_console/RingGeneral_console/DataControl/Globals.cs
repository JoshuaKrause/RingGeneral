using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    static class Globals
    {
        // Data paths.
        public static string DATA = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\RingGeneralData\Data";
        public static string DEFAULT_DATA = DATA + @"\default";
        public static string CURRENT_DATA = DEFAULT_DATA;
        public static string LOG = DATA + @"\log\log.txt";

        // Data prefix standards;
        public static string CharacterPrefix = "CH";

        public static string TagPrefix = "TG";
        public static string ExperiencePrefix = "EX";

        public static string MatchPrefix = "MT";
        public static string AnglePrefix = "AG";

        public static int InitialId = 1000;

        // Minimum match length.
        public static int MinimumMatchDuration = 5;
        public static int MinimumAngleDuration = 2;

        // Swing factor for conflict rolls.
        public static int Swing = 5;

        // Modifiers for varying levels of success and failure.
        public static float PopModifer = 2f;
        public static float TotalSuccessModifier = 1.25f;
        public static float PartialSuccessModifier = 0.75f;
        public static float BotchModifier = 0f;

        // Character enums.
        public enum Gender { Unknown, Male, Female }
        public enum Ethnicity {
            Unknown,
            NorthernEuropean,   // United Kingdom, Germany, Scandinavia, Poland, Russia
            SouthernEuropean,   // Spain, Italy, Greece
            Hispanic,           // Mexico, Central and South America, Caribbean
            MiddleEast,         // Also North Africa
            African,            // Also Caribbean
            SouthAsia,          // Indian, Pakistan
            SouthEastAsia,      // Vietnam, Thailand
            EastAsia,           // China, Korea, Japan
            NativeAmerican,
            Pacific             // Samoa, Fiji
        }

        public enum Nationality
        {
            Unknown,
            UnitedStates,
            Mexico,
            Canada,
            UnitedKingdom,
            Germany
        }
    }
}
