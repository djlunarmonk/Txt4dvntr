using System.Diagnostics.Metrics;
using System.Numerics;
using Txt4dvntr.Classes;

namespace Txt4dvntr
{
    public class Player
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Thing> Inventory { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        Game game { get; set; }


        private int _inventoryCapacity;


        public Player(Game game)
        {
            this.game = game;
            _inventoryCapacity = 7;
            Inventory = new List<Thing> { };
            Name = "Protagonist";
            Description = $"roadworn traveller and adventurer";
        }

        public Player(string name, string description, int x, int y, int inventoryCapacity)
        {
            Name = name;
            Description = description;
            X = x;
            Y = y;
            Inventory = new List<Thing> { };
            _inventoryCapacity = inventoryCapacity;
        }

        public bool Pickup(Thing thing)
        {
            if (Inventory.Count < _inventoryCapacity)
            {
                Inventory.Add(thing);
                return true;
            }
            else
            {
                Console.WriteLine($"You cannot carry this {thing.Handle} unless you drop some other thing.");
                return false;
            }
        }
        public Thing Drop(string thingHandle)
        {
            Thing thing = null;
            if (Inventory.Count != 0)
            {
                thing = Inventory.Where(t => t.Handle == thingHandle).FirstOrDefault();
                Inventory.Remove(thing);
                Console.WriteLine($"You drop the {thing.Handle}");
            }
            else
            {
                Console.WriteLine($"You don't seem to carry what you call {thingHandle}.");
            }
            return thing;
        }

        public void ShowInventory()
        {
            
            if (Inventory.Any())
            {
                string inventoryItems = "You carry ";
                for (int i = 0; i < Inventory.Count; i++)
                {
                    inventoryItems += $"a {Inventory[i].ShortHand}";
                    if (i == Inventory.Count - 1) { inventoryItems += ". "; }
                    else if (i == Inventory.Count - 2) { inventoryItems += " and "; }
                    else { inventoryItems += ", "; }
                }

                int counter = 0;
                foreach (char c in inventoryItems)
                {
                    Console.Write(c);
                    counter++;
                    if (counter > 85 && char.IsWhiteSpace(c))
                    {
                        Console.Write("\n");
                        counter = 0;
                    }
                }
            }
            else { Console.Write("You don't carry anything."); }
            if (Inventory.Count > 0)
            {

                foreach (Thing thing in Inventory)
                {
                    Console.WriteLine();
                }
            
            }
        }

        public string EuroSeparatedLine()
        {
            return $"{Name}€{Description}€{X}€{Y}€{_inventoryCapacity}";
        }

        public bool Move(string direction)
        {
            bool wrongExit = false;

            switch (direction)
            {

                case "north":
                case "n":
                    if (game.worldMap[Y, X].Exits.HasFlag(Exits.north))
                    {
                        Y--;
                        return true;
                    }
                    else { wrongExit = true; }
                    break;
                case "east":
                case "e":
                    if (game.worldMap[Y, X].Exits.HasFlag(Exits.east))
                    {
                        X++;
                        return true;
                    }
                    else { wrongExit = true; }
                    break;
                case "south":
                case "s":
                    if (game.worldMap[Y, X].Exits.HasFlag(Exits.south))
                    {
                        Y++;
                        return true;
                    }
                    else { wrongExit = true; }
                    break;
                case "west":
                case "w":
                    if (game.worldMap[Y, X].Exits.HasFlag(Exits.west))
                    {
                        X--;
                        return true;
                    }
                    else { wrongExit = true; }
                    break;
            }
            if (wrongExit) { Console.WriteLine($"You can't go that way, you can only go: {game.worldMap[Y, X].Exits}"); }
            return false;
        }
    }
}
