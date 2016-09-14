using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RingGeneral_console
{
    static class ModifierControl
    {
        /// <summary>
        /// Applies a modifier to the supplied game object.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="modifier"></param>
        /// <param name="mode"></param>
        static public void AddModifier(IModifiable gameObject, Modifier modifier, string mode = "add")
        {
            if (mode == "add")
            {
                // Check to see if the modifier has prerequisites. 
                // If so, see if they are present in the object's modifier string.
                if (modifier.HasPrerequisites)
                    if (!CheckPrerequisites(gameObject, modifier))
                        throw new GameObjectException("Modifier's prerequisites not present.");

                // Check to see if the modifier is unique.
                // If so, check to ensure that it is not present in the object's modifier string.
                if (modifier.IsUnique)
                    if (CheckUnique(gameObject, modifier))
                        throw new GameObjectException("Unique modifier already applied: "+ modifier.Id);
            }

            // Check to see that the modifier target and the object type match.
            // If so, call the specialized method. If not, throw an exception.
            if (gameObject.GetType() == typeof(Character))
            {
                if (modifier.Target == "Skill" ||
                    modifier.Target == "Relationship" ||
                    modifier.Target == "Gimmick")
                {
                    AddCharacterModifier(gameObject as Character, modifier, mode);
                }
                else
                {
                    throw new GameObjectException("Invalid Character modifier. "+ modifier.Id);
                }
            }

            if (gameObject.GetType() == typeof(Match))
            {
                if (modifier.Target == "Segment" || modifier.Target == "Match")
                {
                    AddMatchModifier(gameObject as Match, modifier, mode);
                }
                else
                {
                    throw new GameObjectException("Invalid Match modifier.");
                }
            }
        }
        
        /// <summary>
        /// Determines the modifier's target. If the modifier is being added, it's added to the appropriate modifier string.
        /// If it is being removed, it is removed from the appropriate modifier string.
        /// Then the modifiers are applied to the selected module object.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="modifier"></param>
        /// <param name="mode"></param>
        static void AddCharacterModifier(Character character, Modifier modifier, string mode)
        {
            if (modifier.Target == "Skill")
            {
                if (mode == "remove")
                {
                    List<string> updatedModifiers = character.SkillModifiers.Split(',').ToList();
                    updatedModifiers.Remove(modifier.Id);
                    character.SkillModifiers = string.Join(",", updatedModifiers);
                }
                else
                {
                    if (string.IsNullOrEmpty(character.SkillModifiers))
                        character.SkillModifiers = modifier.Id;
                    else { character.SkillModifiers = string.Format("{0},{1}", character.SkillModifiers, modifier.Id); }
                }
                ModifyValues(character.Skills, modifier, mode);
                return;
            }

            if (modifier.Target == "Relationship")
            {
                if (mode == "remove")
                {
                    List<string> updatedModifiers = character.RelationshipModifiers.Split(',').ToList();
                    updatedModifiers.Remove(modifier.Id);
                    character.RelationshipModifiers = string.Join(",", updatedModifiers);
                }
                else
                {
                    if (string.IsNullOrEmpty(character.RelationshipModifiers))
                        character.RelationshipModifiers = modifier.Id;
                    else { character.RelationshipModifiers = string.Format("{0},{1}", character.RelationshipModifiers, modifier.Id); }
                }
                ModifyValues(character.Relationships, modifier, mode);
                return;
            }

            if (modifier.Target == "Gimmick")
            {
                if (mode == "remove")
                {
                    List<string> updatedModifiers = character.GimmickModifiers.Split(',').ToList();
                    updatedModifiers.Remove(modifier.Id);
                    character.GimmickModifiers = string.Join(",", updatedModifiers);
                }
                else
                {
                    if (string.IsNullOrEmpty(character.GimmickModifiers))
                        character.GimmickModifiers = modifier.Id;
                    else { character.GimmickModifiers = string.Format("{0},{1}", character.GimmickModifiers, modifier.Id); }
                }
                ModifyValues(character.Gimmick, modifier, mode);
                return;
            }

            else
            {
                throw new GameObjectException("Invalid target type on Character modifier.");
            }                
        }

        static void AddMatchModifier(Match match, Modifier modifier, string mode)
        {
            if (mode == "remove")
            {
                List<string> updatedModifiers = match.StipulationModifiers.Split(',').ToList();
                updatedModifiers.Remove(modifier.Id);
                match.StipulationModifiers = string.Join(",", updatedModifiers);
            }
            else
            {
                if (string.IsNullOrEmpty(match.StipulationModifiers))
                    match.StipulationModifiers = modifier.Id;
                else { match.StipulationModifiers = string.Format("{0},{1}", match.StipulationModifiers, modifier.Id); }
            }
            ModifyValues(match.Stipulations, modifier, mode);
            return;
        }

        /// <summary>
        /// Checks to see if the modifier exists in the gameObject before sending it to the AddModifier method with the
        /// optional "mode" flag set to "remove."
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="modifier"></param>
        static public void RemoveModifier(IModifiable gameObject, Modifier modifier) 
        {
            if (!gameObject.Modifiers.Split(',').ToList().Contains(modifier.Id))
            {
                throw new GameObjectException("Unable to remove modifier. Not applied.");
            }
            else { AddModifier(gameObject, modifier, "remove"); }
        }

        /// <summary>
        /// Add or subtract the modifier's values from the target module's values.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="modifier"></param>
        /// <param name="mode"></param>
        static void ModifyValues(IModule target, Modifier modifier, string mode = "add") 
        {
            PropertyInfo[] targetProperties = target.GetType().GetProperties();

            // Gather the properties from the modifier.
            foreach (PropertyInfo targetProperty in targetProperties)
            {
                PropertyInfo modifierProperty = modifier.GetType().GetProperty(targetProperty.Name);

                // If the remove flag is present, multiply the target's int values by -1.
                // If the property is a boolean, toggle between true and false.
                var modifierValue = modifierProperty.GetValue(modifier, null);
                if (mode=="remove")
                {
                    if (modifierProperty.PropertyType.Equals(typeof(Int32)))
                        modifierValue = (int)modifierValue * - 1;
                    if (modifierProperty.PropertyType.Equals(typeof(bool)))
                    {
                        if ((bool)modifierValue)
                            modifierValue = false;
                        else { modifierValue = true; }
                    }
                }

                // If the modifier's property is a null value, skip it.
                if (modifierProperty.GetValue(modifier) == null)
                    break;

                // If the modifier property's type is an int, add it to the existing value.
                if (modifierProperty.PropertyType.Equals(typeof(Int32)))
                {
                    targetProperty.SetValue(target, (int)targetProperty.GetValue(target) + (int)modifierValue);
                }

                // If the modifier property's type is a boolean, set the target value to it.
                if (modifierProperty.PropertyType.Equals(typeof(bool)))
                {
                    targetProperty.SetValue(target, (bool)modifierValue);
                }

                // Check limits to ensure that the values are within range.
                target.CheckLimits();
            }
        }

        /// <summary>
        /// Checks to see if a prerequisite exists in the modifiable gameObject's modifier string.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        static bool CheckPrerequisites(IModifiable gameObject, Modifier modifier)
        {
            List<string> modifierList = gameObject.Modifiers.Split(',').ToList();
            List<string> prerequisiteList = modifier.Prerequisites.Split(',').ToList();

            foreach (string prerequisite in prerequisiteList)
            {
                if (!modifierList.Contains(prerequisite))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check to see if a unique modifier has already been applied.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        static bool CheckUnique(IModifiable gameObject, Modifier modifier)
        {
            List<string> modifierList = gameObject.Modifiers.Split(',').ToList();

            if (modifierList.Contains(modifier.Id))
                return true;
            return false;
        }

        /// <summary>
        /// Reset a module to its default values.
        /// </summary>
        /// <param name="target"></param>
        static public void ResetModule(IModule target)
        {
            target.ResetModule();
        }

        // Resets an objects modules and reapplies its tag strings.
        static public void RebuildObject(IModifiable gameObject)
        {
            // Determine the class of object and react accordingly.
            if (gameObject.GetType() == typeof(Character))
            {
                Character character = gameObject as Character;

                // Reset the character's modules.
                List<IModule> characterModules = new List<IModule>() { character.Skills, character.Relationships, character.Gimmick };
                foreach (IModule characterModule in characterModules)
                    characterModule.ResetModule();

                // Convert the skills string to lists. If the string is empty, leave the list empty.
                List<string> characterSkills = new List<string>();
                if(!string.IsNullOrEmpty(character.SkillModifiers))
                    characterSkills = character.SkillModifiers.Split(',').ToList();

                List<string> characterRelationships = new List<string>();
                if(!string.IsNullOrEmpty(character.RelationshipModifiers))
                    characterRelationships = character.RelationshipModifiers.Split(',').ToList();

                List<string> characterGimmicks = new List<string>();
                if(!string.IsNullOrEmpty(character.GimmickModifiers))
                    characterGimmicks = character.GimmickModifiers.Split(',').ToList();

                // Empty the modifier strings.
                character.SkillModifiers = string.Empty;
                character.RelationshipModifiers = string.Empty;
                character.GimmickModifiers = string.Empty;

                // Reapply each modifier.
                foreach (string characterSkill in characterSkills)
                {
                    if (characterSkill.Substring(0,2) == "EX")
                        AddModifier(gameObject, DataManager.ExperienceHandler[characterSkill]);
                    else { AddModifier(gameObject, DataManager.TagHandler[characterSkill]); }                  
                }
                foreach (string characterRelationship in characterRelationships)
                    AddModifier(gameObject, DataManager.RelationshipHandler[characterRelationship]);
                foreach (string characterGimmick in characterGimmicks)
                    AddModifier(gameObject, DataManager.GimmickHandler[characterGimmick]);
            }     
            
            if(gameObject.GetType() == typeof(Match))
            {

            }       
        }
    }
}
