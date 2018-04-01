using System;

namespace RingGeneral_console
{
    class Tag : Modifier
    {

        enum TagType { Unknown, Size, Injury, Style, Psychological };
        TagType type;
        public string Type
        {
            get { return type.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    type = default(TagType);
                else
                    type = (TagType)Enum.Parse(typeof(TagType), value);
            }
        }

        // Physical skills.
        public int Brawling { get; set; }
        public int Grappling { get; set; }
        public int Flying { get; set; }
        public int Power { get; set; }

        // Mental skills.
        public int Instinct { get; set; }
        public int Presence { get; set; }
        public int Flair { get; set; }

        // Traits.
        public int Fit_Flabby { get; set; }
        public int Tough_Frail { get; set; }
        public int Aggressive_Calm { get; set; }
        public int Trusting_Suspicious { get; set; }
        public int Leader_Follower { get; set; }
        public int Selfish_Generous { get; set; }
        public int Loyal_Disloyal { get; set; }
        public int Reckless_Conservative { get; set; }
        public int Creative_Dull { get; set; }
    }
}
