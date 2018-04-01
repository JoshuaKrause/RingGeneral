using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    static class ModifierControl
    {

        static public void AddModifier<O,T>(O gameObject, T modifier)
            where O : GameObject
            where T : Modifier
        {
            if (!gameObject.IsModifiable)
                throw new GameObjectException("GameObject is not modifiable.");

            Type type = typeof(O);
            if (modifier.HasPrerequisites)
                return;
                   

            //if (gameObject.Modifiers.Length == 0)
            //    gameObject.Modifiers = modifier.Id;
            //else { gameObject.Modifiers = string.Format("{0},{1}", gameObject.Modifiers, modifier.Id); }
        }
        
        /// <summary>
        /// Adds a modifier directly to a module. Only works with modifiers without prerequisites.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="module"></param>
        /// <param name="modifier"></param>
        static public void AddModifier<T>(Module module, T modifier)
            where T : Modifier
        {
            if (modifier.HasPrerequisites)
                throw new GameObjectException("Modifier has prerequisites. Submit the GameObject instead.");
        }

        static public void RemoveModifier<T>(Character character, T modifier) 
            where T : Modifier
        {
        }

        static void ModifyValues<T>(SkillModule target, T modifier, string mode = "add") 
            where T : Modifier
        {
        }

        static public void ResultModule(SkillModule target)
        {
        }

        static public void RebuildModule(Character character)
        {
        }
    }
}
