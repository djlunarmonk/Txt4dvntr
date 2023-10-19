using Txt4dvntr.Classes;

namespace Txt4dvntr
{
    public static class InterActions
    {
        static Dictionary<string, string> interactions = new Dictionary<string, string>();
        static InterActions()
        {

           // interactions = FileHandler.GetInteractions();
        }
         
        public static void SetNew(Thing thing1, Thing thing2, string action)
        {
            string key = $"{thing1.Handle}&&&{thing2.Handle}";
            if (!interactions.ContainsKey(key))
            {
                interactions.Add(key,action);
            }
        }

        public static void Combine(Thing thing1, Thing thing2)
        {
            string key = $"{thing1.Handle}&&&{thing2.Handle}";
            if (interactions.ContainsKey(key))
            {
                string action = interactions[key];
                Action(thing1, thing2, action);
            }
            else { Console.WriteLine("Nothing happens."); }
        }

        public static void Action(Thing thing1, Thing thing2, string action)
        {
            switch (action)
            {
                case "openObstruction":
                    if ((thing1 is Solution) && (thing2 is Obstruction))
                    {
                        if ((thing1 as Solution).id == (thing2 as Obstruction).id)
                        {
                            (thing2 as Obstruction).OnOpen();
                        }
                    }

                    break;

                case "openTube":
                    if ((thing1 is Thing) && (thing2 is Thing))
                    {
                        Thing thing3 = Program.game.worldMap[0,0].Inventory.Where(t => t.Handle == "tube").FirstOrDefault();
                        if (thing3 is not null)
                        {
                            Game.Print($"A lid pops out of the {thing2.Handle} and rolls into a tiny crack.");
                            Program.game.player.Inventory.Remove(thing2);
                            Program.game.worldMap[Program.game.player.Y, Program.game.player.X].Inventory.Remove(thing2);
                            Program.game.player.Inventory.Add(thing3); 
                        }
                    }
                    break;
                case "openContainer":
                    if ((thing1 is Thing) && (thing2 is Container))
                    {

                    }
                    break;

            }
        }

    }
}
