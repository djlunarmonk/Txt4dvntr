using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txt4dvntr.Classes
{
    public class Container : Thing
    {
        private int _capacity;
        public List<Thing> Inventory;

        public Container(string handle, string shorthand, string description, string abilities, int capacity=1) : base(handle, shorthand, description, abilities)
        {
            Inventory = new List<Thing>();
            _capacity = capacity;
        }

        public override void Examine()
        {
            string description = ($"It's a {Description}. It holds ");
            if ( Inventory.Count > 0 )
            {
                for (int i =0; i < Inventory.Count; i++)
                {
                    description += $"a {Inventory[i].ShortHand}";
                    if (i == Inventory.Count - 1) { description += ". "; }
                    else if (i == Inventory.Count - 2) { description += " and "; }
                    else { description += ", "; }
                }
            }
            else { description += $"nothing. "; }
            Game.Print(description);
        }


        public void TakeFrom(Player player, string objective)
        {
            if ( Inventory.Count == 0 ) { Console.WriteLine($"The {ShortHand} is empty. "); }
            else
            {
                List<Thing> list = Inventory.Where(t => t.Handle == objective).ToList();
                if ( list.Count == 0 ) { Console.WriteLine($"There doesn't seem to be a {objective} in the {Handle}. "); }
                if (list.Count > 1) { Console.WriteLine($"I can't decide on which {objective} to take from the {Handle}. "); }
                else if (list.Count == 1 && list[0].Abilities == Abilities.WillTravel)
                {
                    if (player.Pickup(list[0])) { Inventory.Remove(list[0]); Console.WriteLine("Ok."); }
                }
                else { Console.WriteLine("Sorry, but no."); }
            }
        }

        public bool PutIn(Thing thing)
        {
            if (Inventory.Count < _capacity)
            {
                Inventory.Add(thing);
                Console.WriteLine("Ok.");
                return true;
            }
            else
            {
                Console.WriteLine($"The {ShortHand} is full.");
                return false;
            }
        }
    }
}
