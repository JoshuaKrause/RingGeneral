using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    static class DataManager
    {
        static public Dictionary<string, Character> CharacterHandler;

        static public Dictionary<string, Tag> TagHandler;
        static public Dictionary<string, Experience> ExperienceHandler;
        //static public Dictionary<string, Tag> GimmickHandler = new Dictionary<string, Tag>();
        //static public Dictionary<string, Tag> RelationshipHandler = new Dictionary<string, Tag>();

        static public Dictionary<string, Match> MatchHandler = new Dictionary<string, Match>();
        static public Dictionary<string, Angle> AngleHandler = new Dictionary<string, Angle>();

        public static void InitializeData()
        {
            // Initialize tags.
            TagHandler = DataSerializer.Deserialize<Tag>(DataAccessor.ReadDatabase("tag"));
            ExperienceHandler = DataSerializer.Deserialize<Experience>(DataAccessor.ReadDatabase("experience"));
            //GimmickHandler = DataSerializer.Deserialize<Tag>(DataAccessor.ReadDatabase("gimmick"));

            // Characters must be initialized after tags.
            CharacterHandler = DataSerializer.Deserialize<Character>(DataAccessor.ReadDatabase("character"));
        }
    }
}
