using System;
using System.Collections.Generic;
using System.Linq;

namespace RingGeneral_console
{
    class Character : GameObject
    {
        // Basic identity information.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AliasLong { get; set; }
        public string AliasShort { get; set; }

        // Image and description.
        public string Image { get; set; }
        public string Description { get; set; }

        // Gender, Nationality, Ethnicity, Height and Weight.
        Globals.Gender gender { get; set; }
        public string Gender
        {
            get { return gender.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    gender = default(Globals.Gender);
                else
                    gender = (Globals.Gender)Enum.Parse(typeof(Globals.Gender), value);
            }
        }

        Globals.Nationality nationality { get; set; }
        public string Nationality
        {
            get { return nationality.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    nationality = default(Globals.Nationality);
                else
                    nationality = (Globals.Nationality)Enum.Parse(typeof(Globals.Nationality), value);
            }
        }

        Globals.Ethnicity ethnicity { get; set; }
        public string Ethnicity
        {
            get { return ethnicity.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    ethnicity = default(Globals.Ethnicity);
                else
                    ethnicity = (Globals.Ethnicity)Enum.Parse(typeof(Globals.Ethnicity), value);
            }
        }
        public int Height { get; set; }
        public int Weight { get; set; }

        // Birthday, hometown, and debut.
        DateTime birthday;
        public string Birthday
        {
            get { return string.Format("{0:MM/dd/yy}", birthday); }
            set { birthday = DateTime.Parse(value); }
        }

        public string Hometown { get; set; }

        DateTime debut; 
        public string Debut
        {
            get { return string.Format("{0:MM/dd/yy}", debut); }
            set { debut = DateTime.Parse(value); }
        }

        // Skills, Relationships, and Gimmicks.
        public string SkillModifiers { get; set; }
        public SkillModule Skills = new SkillModule();

        public string RelationshipModifiers { get; set; }
        public RelationshipModule Relationships = new RelationshipModule();

        public string GimmickModifiers { get; set; }
        public GimmickModule Gimmick = new GimmickModule();

        public Character()
        {
            IsModifiable = true;
        }
    }
}

