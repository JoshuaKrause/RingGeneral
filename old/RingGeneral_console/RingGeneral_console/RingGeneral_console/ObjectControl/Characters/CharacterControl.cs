using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RingGeneral_console
{

    /// <summary>
    /// Modifies characters through the addition and subtraction of tags.
    /// </summary>
    static class CharacterControl 
    {
        /// <summary>
        /// Adds a modifier by appending it to the existing tag string and submitting it
        /// to the ModifyValues() method.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="tagId"></param>
        static public void AddModifier<T>(Character character, T modifier) where T : Modifier
        {
            if (character.SkillModifiers.Length == 0)
                character.SkillModifiers = modifier.Id;
            else { character.SkillModifiers = string.Format("{0},{1}", character.SkillModifiers, modifier.Id); }            
            ModifyValues(character.Skills, modifier);
        }

        /// <summary>
        /// Removes a modifier by removing it from the existing tag string and submitting it
        /// to the ModifyValues() method.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="tagId"></param>
        static public void RemoveModifier<T>(Character character, T modifier) where T : Modifier
        {
            List<string> modifierList = character.SkillModifiers.Split(',').ToList();
            modifierList.Remove(modifier.Id);
            character.SkillModifiers = string.Join(",", modifierList);

            // Note the mode flag, which tells the ModifyValues() method to reverse the math.
            ModifyValues(character.Skills, modifier, "remove");
        }
        
        /// <summary>
        /// Applies a tag's modifiers to a Skills object using reflection.
        /// The optional 'mode' flag allows a tag to be applied or reversed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="modifier"></param>
        /// <param name="mode"></param>
        static void ModifyValues<T>(SkillModule target, T modifier, string mode = "add") where T : Modifier
        {
            // Get the Properties of the supplied tag. Skills and traits are kept as Int32 Properties.
            Type modifierType = typeof(T);
            PropertyInfo[] modifierProperties = modifierType.GetProperties().Where(prop => prop.PropertyType == typeof(Int32)).ToArray();

            // Find the matching Properties in the Skills object and apply the modifiers.
            foreach (PropertyInfo modifierProperty in modifierProperties)
            {
                PropertyInfo targetProperty = typeof(SkillModule).GetProperty(modifierProperty.Name);
                int modifierValue;
                if (mode == "remove")
                    modifierValue = (int)modifierProperty.GetValue(modifier, null) * -1;
                else
                {
                    modifierValue = (int)modifierProperty.GetValue(modifier, null);
                }

                // Skills and traits have a floor of 1 and a ceiling of 100.
                int newValue = (int)targetProperty.GetValue(target, null) + modifierValue;
                if (newValue > 100)
                    newValue = 100;
                if (newValue < 0)
                    newValue = 0;

                targetProperty.SetValue(target, newValue);
            }            
        }
        
        /// <summary>
        /// Resets all target values to their floor.
        /// </summary>
        /// <param name="target"></param>
        static public void ResetValues(SkillModule target)
        {
            PropertyInfo[] targetProperties = typeof(SkillModule).GetProperties().Where(prop => prop.PropertyType == typeof(Int32)).ToArray();
            foreach (PropertyInfo targetProperty in targetProperties)
                targetProperty.SetValue(target, 0);
        }
 
        /// <summary>
        /// Initializes a character by resetting skills and reapplying tags.
        /// </summary>
        /// <param name="character"></param>
        static public void InitializeCharacter(Character character)
        {
            ResetValues(character.Skills);
            List<string> modifierList = character.SkillModifiers.Split(',').ToList();
            character.SkillModifiers = "";

            foreach (string tagId in modifierList)
            {
                if (tagId.Substring(0, 2) == "EX")
                    AddModifier(character, DataManager.ExperienceHandler[tagId]);
                else
                {
                    AddModifier(character, DataManager.TagHandler[tagId]);
                }
            }
        }
    }
}

