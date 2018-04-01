using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    class Character : GameObject, IExportable
    {
        // Define enumerations.
        public enum Genders { Male, Female };
        public enum Nationalities { UnitedStates, Canada, Mexico };

        // Main character properties.
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AliasLong { get; set; }
        public string AliasShort { get; set; }

        public string Description { get; set; }

        public Genders Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public Nationalities Nationality { get; set; }
        public string Hometown { get; set; }

        public List<Tag> Tags = new List<Tag>();
        public Attributes Attributes = new Attributes();

        public Character()
        {
        }

        public Character(Dictionary<string, string> propertyDict)
        {            
            try
            {
                Id = propertyDict["Id"];

                FirstName = propertyDict["FirstName"];
                LastName = propertyDict["LastName"];
                AliasLong = propertyDict["AliasLong"];
                AliasShort = propertyDict["AliasShort"];
                Description = propertyDict["Description"];

                Gender = (Genders)Enum.Parse(typeof(Genders), propertyDict["Gender"]);
                BirthDate = Convert.ToDateTime(propertyDict["BirthDate"]);
                Height = Convert.ToInt32(propertyDict["Height"]);
                Weight = Convert.ToInt32(propertyDict["Weight"]);

                Nationality = (Nationalities)Enum.Parse(typeof(Nationalities), propertyDict["Nationality"]);
                Hometown = propertyDict["Hometown"];
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddTag(Tag tag)
        {
            Attributes.AddAttributes(tag.Attributes);
            Tags.Add(tag);
        }

        public void SubtractTag(Tag tag)
        {
            if (!Tags.Contains(tag))
                throw new Exception("Character does not have this tag.");
            else
            {
                Attributes.SubtractAttributes(tag.Attributes);
                Tags.Remove(tag);
            }
        }

        public string GetTagNames()
        {
            return string.Join(",", Tags.Select(o => o.Name).ToList());
        }

        public string Export()
        {
            StringBuilder output = new StringBuilder();

            output.AppendFormat("{0}={1};\n", "Id", Id);
            output.AppendFormat("\t{0}={1};\n", "FirstName", FirstName);
            output.AppendFormat("\t{0}={1};\n", "LastName", LastName);
            output.AppendFormat("\t{0}={1};\n", "AliasLong", AliasLong);
            output.AppendFormat("\t{0}={1};\n", "AliasShort", AliasShort);
            output.AppendFormat("\t{0}={1};\n", "Description", Description);
            output.AppendFormat("\t{0}={1};\n", "Gender", Gender.ToString());
            output.AppendFormat("\t{0}={1};\n", "BirthDate", BirthDate.ToString("MM/dd/yyyy"));
            output.AppendFormat("\t{0}={1};\n", "Height", Height);
            output.AppendFormat("\t{0}={1};\n", "Weight", Weight);
            output.AppendFormat("\t{0}={1};\n", "Nationality", Nationality.ToString());
            output.AppendFormat("\t{0}={1};\n", "Hometown", Hometown);

            return output.ToString();
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append(Export());
            output.Append("Tags: " + GetTagNames() + "\n");
            output.Append(Attributes);

            return output.ToString();
        }
    }
}
