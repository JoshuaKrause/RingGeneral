using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class Modifier : GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // What module does this modifier target?
        enum ModuleTarget { Unknown, Skill, Relationship, Bonus, Segment };
        ModuleTarget target { get; set; }
        public string Target
        {
            get { return target.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    target = default(ModuleTarget);
                else
                    target = (ModuleTarget)Enum.Parse(typeof(ModuleTarget), value);
            }
        }

        // If a Tag is Unique, it does not stack.
        public bool Unique { get; set; }
        public bool HasPrerequisites { get; set; }
    }
}
