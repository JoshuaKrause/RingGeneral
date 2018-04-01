using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    class Character : GameObject
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

        public Attributes Attributes;

        public Character(Dictionary<string, string> propertyDict = null)
        {
            Attributes = new Attributes();

            if (propertyDict != null)
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
        }

        public override string Export()
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
            return Export();
        }
    }
}
