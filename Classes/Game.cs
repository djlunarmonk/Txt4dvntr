﻿namespace Txt4dvntr.Classes
{

    public class Game
    {
        public bool keepPlaying;
        private bool _movement;
        public Player player;
        public MapNode[,] worldMap;

        public Game()
        {
            PrepareGame();
        }

        public void PrepareGame()
        {
            //worldMap = Repository.GetWorldMap(3, 3);
            //player = new Player(this);
            //// Start in the middle of the map
            //player.Y = worldMap.GetLength(0) / 2;
            //player.X = worldMap.GetLength(1) / 2;

            //worldMap = MapMaker.GetWorldMap(9, 9);

            worldMap = new MapNode[9, 9];
            FileHandler.ReviveMap(ref worldMap);
            
            player = new Player(this);
            player.Y = 7;
            player.X = 4;

        }

        public void RunGame()
        {
            MapMaker.RepopulateWorld(ref worldMap);

            keepPlaying = true;
            _movement = true;
            do
            {
                if (_movement) worldMap[player.Y, player.X].Visit();
                if (keepPlaying)
                {
                    Console.Write("\n> ");
                    string input = Console.ReadLine().Trim();
                    ParseAndExecute(input);
                }
            }
            while (keepPlaying);
            FileHandler.SaveGame(ref worldMap, ref player);
            Console.ReadLine();
        }

        public void ParseAndExecute(string userInput)
        {
            bool ok = false;

            if (userInput != "")
            {
                char[] delimiters = new char[] { ' ', '.', ',', '!', '?' };
                string[] input = userInput.ToLower().Split(delimiters, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                #region verbs

                #endregion

                _movement = false;

                if (input.Length > 0)
                {
                    List<Thing> items = new List<Thing>();

                    switch (input[0])
                    {
                        #region developer "tools"
                        //debugging world setup
                        case "exitsall":
                            for (int i = 0; i < worldMap.GetLength(0); i++)
                            {
                                for (int j = 0; j < worldMap.GetLength(1); j++)
                                {
                                EditExits(i,j);
                                }
                            }
                            break;
                        case "exitshere":
                            EditExits(player.X, player.Y);
                            worldMap[player.Y, player.X].Display();
                            Print("Better?");
                            break;

                        case "teleport":
                            if (input.Length == 3)
                            {
                                int xx, yy;
                                if (int.TryParse(input[1], out xx) && int.TryParse(input[2], out yy))
                                {
                                    if (yy >= 0 && yy < worldMap.GetLength(0) && xx >= 0 && xx < worldMap.GetLength(1))
                                    {
                                        player.Y = yy;
                                        player.X = xx;
                                        _movement = true;
                                    }
                                }
                                else Print("Not like that.");
                            }
                            else Print("Not like that.");

                            break;
                        #endregion

                        // Talking
                        case "say":

                            if (input.Length > 1)
                            {
                                Console.WriteLine($"You say: \"{userInput.Substring(4).Trim()}\"");

                            }
                            else Print("What did you call me?");
                            break;

                        // Quitting
                        case "quit":
                        case "exit":
                            Console.WriteLine("Goodbye!");
                            keepPlaying = false;
                            break;

                        // Moving around
                        case "go":
                        case "walk":
                        case "move":
                            if (input.Length > 1)
                            {
                                ok = _movement = player.Move(input[1]);
                            }
                            else { Console.WriteLine($"{char.ToUpper(input[0][0])}{input[0].Substring(1)} in which direction?"); }
                            break;

                        // Examination
                        case "examine":
                        case "look":

                            if (input.Length > 1)
                            { 
                                items = (from thing in worldMap[player.Y, player.X].Inventory
                                                     where thing.Handle == input[1]
                                                     select thing).ToList();
                                if (items.Count == 0) { items.AddRange(player.Inventory.Where(t => t.Handle == input[1]).ToList()); }
                                if (items.Count == 1) { items[0].Examine(); }
                                else if (items.Count > 1) { Console.WriteLine("Which one?"); }
                                else if (items.Count == 0) { Console.WriteLine($"There is no {input[1]} here!"); }
                            }
                            else { worldMap[player.Y, player.X].Display(); }
                            break;

                        // Selfinspection?
                        case "inventory":

                            player.ShowInventory();
                            break;

                        // Pickup-lines    
                        case "take":
                        case "grab":
                        case "pick":
                        case "get":

                            // ofullständigt kommando
                            if (input.Length == 1 || (input.Length == 2 && input[1] == "up")) { Console.WriteLine($"What do you want to {input[0]}?"); }

                            string objective = "";
                            
                            if (input.Length > 1)
                            {
                                if (input[0] == "pick" && input[1] == "up")
                                {
                                    input[0] = "pick up";
                                    if (input.Length >= 3) { objective = input[2]; }
                                    else { Console.WriteLine("Who's calling?"); break; }
                                }
                                else { objective = input[1]; }
                            }
                            else { Console.WriteLine($"{char.ToUpper(input[0][0])}{input[0].Substring(1)} what?"); break; }


                            //if (worldMap[player.Y, player.X].Inventory.Count == 0) { Print($"There's nothing in this room to {input[0]}"); }
                            
                            bool container = false;

                            if (userInput.Contains("from") && input.Length > 4)
                            {
                                items = worldMap[player.Y, player.X].Inventory.Where(t => (t.Handle.Equals(input[4]) && (t is Container))).ToList();
                                if (items.Count == 1) container = true;
                                else items = player.Inventory.Where(t => (t.Handle.Equals(input[4]) && (t is Container))).ToList();
                                if (items.Count == 1) container = true;

                            }
                            else if (userInput.Contains("from") && input.Length > 3)
                            {
                                items = worldMap[player.Y, player.X].Inventory.Where(t => (t.Handle.Equals(input[3]) && (t is Container))).ToList();
                                if (items.Count == 1) container = true;
                                else items = player.Inventory.Where(t => (t.Handle.Equals(input[3]) && (t is Container))).ToList();
                                if (items.Count == 1) container = true;
                            }
                            else
                            {
                                items = (from thing in worldMap[player.Y, player.X].Inventory
                                                where thing.Handle == objective
                                                select thing).ToList();

                            }
                            if (items.Count == 1 && container)
                            {
                                (items[0] as Container).TakeFrom(player, objective);
                            }
                            
                            else if (items.Count == 1 && items[0].Abilities == Abilities.WillTravel)
                            {
                                if (!container && player.Pickup(items[0]))
                                {
                                    worldMap[player.Y, player.X].Inventory.Remove(items[0]);
                                    ok = true;
                                }
                            }
                            else if (items.Count == 1) { Console.WriteLine($"This {objective} cannot be taken from here."); }
                            else if (items.Count > 1) { Console.WriteLine("Which one?"); }
                            else if (items.Count == 0) { Console.WriteLine($"There is no {objective} to {input[0]}!"); }
                        
                            
                            break;

                        // Drop it like it's hot
                        case "drop":
                            if (input.Length > 1)
                            {
                                Thing droppedThing = player.Drop(input[1]);
                                if (droppedThing != null)
                                {
                                    worldMap[player.Y, player.X].Inventory.Add(droppedThing);
                                    ok = true;
                                }
                            }
                            else { Print($"If you want to give up, there are other commands for that."); }
                            break;

                        // Using stuff
                        case "use":
                            if (input.Length == 1) { Print($"Use what?"); break; }

                            items = player.Inventory.Where(t => t.Handle == input[1]).ToList();

                            // Using a marker
                            if (items.Count == 1 && items[0] is Marker)
                            {
                                Marking marking = (items[0] as Marker).MarkThisPlace();
                                if (marking is not null) { worldMap[player.Y, player.X].Inventory.Add(marking); }
                            }

                            // Using something on something else
                            if (input.Length > 3 && input[2] == "on")
                            {
                                if (items.Count == 0)
                                {
                                    items = worldMap[player.Y, player.X].Inventory.Where(t => t.Handle == input[1]).ToList();
                                    if (items.Count == 0) { Print($"There is no {input[1]} around to use. "); }
                                }
                                else if (items.Count == 1) { items.AddRange(worldMap[player.Y, player.X].Inventory.Where(t => t.Handle == input[3]).ToList()); }
                                if (items.Count == 1) { items.AddRange(player.Inventory.Where(t => t.Handle == input[3]).ToList()); }
                                if (items.Count == 2) { InterActions.Combine(items[0], items[1]); }
                                else { Print("Your wish isn't a readable command."); }
                            }
                            else if (input.Length > 2)
                            {
                                items = player.Inventory.Where(t => t.Handle == input[1]).ToList();
                                if (items.Count == 0) { items = worldMap[player.Y, player.X].Inventory.Where(t => t.Handle == input[1]).ToList(); }
                                if (items.Count == 0) { Print($"There is no {input[1]} around to use. "); }
                            }
                            
                            break;

                        default:
                            ok = _movement = player.Move(input[0]);
                            if (!ok) { Console.WriteLine("Come again?"); }
                            break;
                    }
                    if (ok) { Console.WriteLine("Ok.\n"); }
                }
            }

        }

        public static void Print(string summary)
        {
            int lineBreak = 95;
            try
            {
                lineBreak = Console.WindowWidth - 5;
            }
            catch (Exception e)
            {
                lineBreak = 95;
            }

            int charNo = 0;
            for (int i = 0; i < summary.Length; i++)
            {
                char c = summary[i];
                charNo++;
                if ((charNo > lineBreak - 10) && char.IsWhiteSpace(c))
                {
                    Console.Write("\n");
                    charNo = 0;
                }
                else if (charNo == 1 && char.IsWhiteSpace(c))
                {
                    ;
                }
                else { Console.Write(c); }
                
            }
            Console.WriteLine();
        }

        public void EditExits(int xx, int yy)
        {
            int i = xx;
            int j = yy;
            Console.Write("\nAdd exits: ");
            string add = Console.ReadLine().Trim();
            Console.Write("\nRemove exits:");
            string rem = Console.ReadLine().Trim();

            string[] choppedAdd = add.Split(" ");

            foreach (string c in choppedAdd)
            {
                switch (c)
                {
                    case "n":
                        worldMap[j, i].Exits |= Exits.north; break;
                    case "ne":
                        worldMap[j, i].Exits |= Exits.northeast; break;
                    case "e":
                        worldMap[j, i].Exits |= Exits.east; break;
                    case "se":
                        worldMap[j, i].Exits |= Exits.southeast; break;
                    case "s":
                        worldMap[j, i].Exits |= Exits.south; break;
                    case "sw":
                        worldMap[j, i].Exits |= Exits.southwest; break;
                    case "w":
                        worldMap[j, i].Exits |= Exits.west; break;
                    case "nw":
                        worldMap[j, i].Exits |= Exits.northwest; break;
                }
            }

            string[] choppedRemove = rem.Split(" ");
            foreach (string c in choppedRemove)
            {
                switch (c)
                {
                    case "n":
                        worldMap[j, i].Exits &= ~Exits.north; break;
                    case "ne":
                        worldMap[j, i].Exits &= ~Exits.northeast; break;
                    case "e":
                        worldMap[j, i].Exits &= ~Exits.east; break;
                    case "se":
                        worldMap[j, i].Exits &= ~Exits.southeast; break;
                    case "s":
                        worldMap[j, i].Exits &= ~Exits.south; break;
                    case "sw":
                        worldMap[j, i].Exits &= ~Exits.southwest; break;
                    case "w":
                        worldMap[j, i].Exits &= ~Exits.west; break;
                    case "nw":
                        worldMap[j, i].Exits &= ~Exits.northwest; break;
                }
            }
        }

    }
}

