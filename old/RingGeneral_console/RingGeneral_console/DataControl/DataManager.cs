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
        static public Dictionary<string, Gimmick> GimmickHandler;
        static public Dictionary<string, Relationship> RelationshipHandler;

        static public Dictionary<string, Match> MatchHandler = new Dictionary<string, Match>();
        static public Dictionary<string, Angle> AngleHandler = new Dictionary<string, Angle>();
        static public Dictionary<string, Stipulation> StipulationHandler;

        public static void InitializeData()
        {
            // Initialize tags.
            TagHandler = DataSerializer.Deserialize<Tag>(DataAccessor.ReadDatabase("tag"));
            ExperienceHandler = DataSerializer.Deserialize<Experience>(DataAccessor.ReadDatabase("experience"));
            GimmickHandler = DataSerializer.Deserialize<Gimmick>(DataAccessor.ReadDatabase("gimmick"));
            RelationshipHandler = DataSerializer.Deserialize<Relationship>(DataAccessor.ReadDatabase("relationship"));

            // Characters must be initialized after tags.
            CharacterHandler = DataSerializer.Deserialize<Character>(DataAccessor.ReadDatabase("character"));

            StipulationHandler = DataSerializer.Deserialize<Stipulation>(DataAccessor.ReadDatabase("stipulation"));
        }

        public static void SaveAllDatabases()
        {
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.CharacterHandler.Values.ToList()), "character");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.TagHandler.Values.ToList()), "tag");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.ExperienceHandler.Values.ToList()), "experience");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.RelationshipHandler.Values.ToList()), "relationship");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.GimmickHandler.Values.ToList()), "gimmick");

            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.MatchHandler.Values.ToList()), "match");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.AngleHandler.Values.ToList()), "angle");
            DataAccessor.SaveDatabase(DataSerializer.Serialize(DataManager.StipulationHandler.Values.ToList()), "stipulation");
        }
    }
}
