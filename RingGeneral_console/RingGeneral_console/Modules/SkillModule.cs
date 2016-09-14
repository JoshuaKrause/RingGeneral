using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RingGeneral_console
{
    class SkillModule : IModule
    {
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


        public SkillModule()
        {
            ResetModule();
        }

        /// <summary>
        /// Resets the module to default values.
        /// </summary>
        public void ResetModule()
        {
            int i = 0;
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (i < 7)
                    property.SetValue(this, 0);
                else { property.SetValue(this, 50); }
                i++;
            }
        }

        public void CheckLimits()
        {
            foreach (PropertyInfo property in GetType()
                
                .GetProperties())
            {
                if ((int)property.GetValue(this) > 100)
                    property.SetValue(this, 100);
                if ((int)property.GetValue(this) < 0)
                    property.SetValue(this, 0);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                output.AppendFormat("{0}: {1}\n", property.Name, property.GetValue(this));
            }
            return output.ToString();
        }
    }
}
