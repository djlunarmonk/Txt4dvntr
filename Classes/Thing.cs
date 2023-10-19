using System;

namespace Txt4dvntr.Classes
{
    [Flags]
    public enum Abilities
    {
        None = 0,
        WillTravel = 1,
        HasUse = 2,
        CanInteract = 4
    }

    public class Thing
    {
        public string Handle { get; set; }
        public string ShortHand { get; set; }
        public string Description { get; set; }
        public Abilities Abilities { get; set; }

        public Thing(string handle, string shorthand, string description, string abilities)
        {
            Handle = handle;
            ShortHand = shorthand;
            Description = description;
            int intValue = Convert.ToInt32(abilities, 2);
            Abilities = (Abilities)intValue;
        }

        public virtual void Examine()
        {
            Game.Print($"It's a {Description}.");
        }

       
        public string EuroSeparatedLine()
        {
            string esl = $"{Handle}€{ShortHand}€{Description}€{Convert.ToString((int)Abilities, 2)}";

            return esl;
        }



    }
}
