using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    class Tag : GameObject, IExportable
    {
        public string Name { get; set; }

        public Attributes Attributes;

        public Tag(Dictionary<string, string> propertyDict)
        {
            Attributes = new Attributes();

            Id = propertyDict["Id"];
            Name = propertyDict["Name"];

            foreach (string attribute in Attributes.keyList)
            {
                if (propertyDict.ContainsKey(attribute))
                    Attributes.AttributeDict[attribute] = Int32.Parse(propertyDict[attribute]);
            }
        }

        public string Export()
        {
            StringBuilder output = new StringBuilder();

            output.AppendFormat("{0}={1};\n", "Id", Id);
            output.AppendFormat("\t{0}={1};\n", "Name", Name);

            foreach (KeyValuePair<string, int> entry in Attributes.AttributeDict)
            {
                if (entry.Value != 0)
                    output.AppendFormat("\t{0}={1};\n", entry.Key, entry.Value);
            }

            return output.ToString();
        }

        public override string ToString()
        {
            return Export();
        }
    }
}
