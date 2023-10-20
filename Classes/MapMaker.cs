namespace Txt4dvntr.Classes
{
    public static class MapMaker
    {
        #region old map generator

        //public static MapNode[,] GetWorldMap(int xx, int yy)
        //{
        //    MapNode[,] worldMap = new MapNode[yy, xx];


        //    for (int x = 0; x < worldMap.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < worldMap.GetLength(1); y++)
        //        {
        //            // Making the id from current coordinates
        //            string id = $"{x.ToString("x2")}{y.ToString("x2")}";


        //            // Making a generic description and switching on exits
        //            //string description = "This seems to be an outdoor labyrinth? At least it has high stone walls and no roofing. " +
        //            //                        "I guess it could be an art project as well. Time might tell, " +
        //            //                        "if you live that long. This place smells like death.";
        //            string description = "Outdoor labyrinth?";

        //            Exits exits = new Exits();

        //            switch (y)
        //            {
        //                case 0:
        //                case 8:
        //                    exits |= Exits.nowhere;
        //                    description = "This is the ocean. Sink or swim! ";
        //                    break;
        //                case 1:
        //                    if (x == 1 || x == 5 || x == 7) { exits |= Exits.south; if (x != 7) exits |= Exits.east; }
        //                    if (x == 2 || x == 3 || x == 6 || x == 7) { exits |= Exits.west; }
        //                    description += "Is it an ocean you hear to the north? ";
        //                    break;
        //                case 7:
        //                    if (y == 3 || y == 4) { exits |= Exits.north; }
        //                    if (y != 3 && y != 7) { exits |= Exits.east; }
        //                    if (y != 1 && y != 4) { exits |= Exits.west; }
        //                    description += "You think you can hear an ocean to the south. ";
        //                    break;
        //                default:
        //                    exits |= Exits.south | Exits.north;
        //                    break;
        //            }

        //            switch (x)
        //            {
        //                case 0:
        //                case 8:
        //                    exits |= Exits.nowhere;
        //                    description = "This is the ocean. Sink or swim! ";
        //                    break;
        //                case 1:
        //                    if (y == 1 || y == 7) { exits |= Exits.east; }
        //                    if (y != 1) exits |= Exits.north;

        //                    description += "This sounds like a west coast beach. Or being trapped in a bunker on the west coast.";
        //                    break;
        //                case 7:
        //                    if (y == 7) { exits |= Exits.west; }
        //                    description += "Is it possible? Sounds like waves to the east. ";
        //                    break;
        //                default:
        //                    exits |= Exits.west | Exits.east;
        //                    break;
        //            }
        //            List<Thing> inventory = new List<Thing>();

        //            worldMap[y, x] = new MapNode(id, description, inventory, exits);
        //        }
        //    }
        //    return worldMap;
        //}
        #endregion

        public static void RepopulateWorld(ref MapNode[,] worldMap)
        { 

            Obstruction door = new Obstruction("door", "large wooden door",
                                                $"large wooden door in a rustique style, with black metal details but no ingravings. It has a keyhole. ",
                                                "0", ref worldMap[1, 4], "The door opens", Exits.north,
                                                "key", "rusty key", "rusty key, nothing special about it");
            Thing key = worldMap[1, 4].Inventory.Where(t => t.Handle == "key").SingleOrDefault();
            if (key is not null)
            {
                Container outerDoll = new Container("doll", "Russian doll", "wooden doll painted like an archaic Greek amphora, perhaps it's more accurate to call it a greek doll", "1", 1);
                Container innerDoll = new Container("dolly", "smaller Russian dolly", "small wooden dolly. It has a strong smell of laquer and it has a traditional decor painted with beautiful colours", "1", 1);
                innerDoll.Inventory.Add(key);
                InterActions.SetNew(key, door, "openObstruction");
                worldMap[1, 4].Inventory.Remove(key);
                outerDoll.Inventory.Add(innerDoll);
                worldMap[1, 3].Inventory.Add(outerDoll);
            }
            
            Obstruction wall = new Obstruction("wall", "wall full of holes",
                                                $"wall not so well made. The person who made this wall was possibly the weakest link of a chain gang, there are a lot of holes. " +
                                                $"Sadly not big enough to crawl through though. Not like you could stick a crowbar into one of the holes and pry it open either",
                                                "0", ref worldMap[2, 4], "A part of the wall falls as the dynamite explodes", Exits.north,
                                                "dynamite", "stick that sort of resembles dynamite", "stick which surface has a somewhat rough and papery feel to it, " +
                                                "it smells funny (not haha-funny) and a rather short fuse sticks out one of its ends");

            Thing dynamite = worldMap[2, 4].Inventory.Where(t => t.Handle == "dynamite").SingleOrDefault();
            if (dynamite is not null)
            {
                Thing rod = new Thing("rod", "short metal rod", "short metal rod, not too heavy. Or possibly a tube? You know the kind of thing you'd collect coins in, " +
                                                   "only it has no slit to insert said coins into. It does however have a groove in its rear end, had you've had a large " +
                                                   "screwdriver on you, you could've tried manipulating it", "1");

                Container coinTube = new Container("tube", "short metal tube", "short metal tube, not too heavy. You know the kind of thing you'd collect coins in, " +
                                                   "only it has no slit to insert said coins into. It did however have a lid in its rear end, but you lost it as it came off", "1", 1);

                Container corpse = new Container("corpse", "rotting corpse", "thing no person in their right mind would think to be reality, " +
                                                "but you can't argue with the omnious ominous stench of stale meat. It's a bull's head on a rather large person's body. It's the rotting corpse, " +
                                                "or carcass if you will, of a minotaur", "0", 2);
                Thing coin = new Thing("coin", "silver coin", "coin made of well maybe not silver, but... It has cyrillic lettering, you can't read its value or even tell whether it's antique " +
                                        "or just somewhat old. It's not a Euro cent coin anyway. In some ancient myths they placed coins on the dead person's eyelids at funerals, something to " +
                                        "pay the ferryman with. Wonder if there were two coins on the minotaur? The other could have just sunk into the eye? Or someone else may have taken it. " +
                                        "You don't know where you are and you might not be as alone as you feel", "1");

                corpse.Inventory.Add(coin);
                InterActions.SetNew(coin, rod, "openTube");
                worldMap[5,5].Inventory.Add(corpse);
                coinTube.Inventory.Add(dynamite);
                InterActions.SetNew(dynamite, wall, "openObstruction");

                worldMap[2, 4].Inventory.Remove(dynamite);
                worldMap[0, 0].Inventory.Add(coinTube);
                worldMap[2, 6].Inventory.Add(rod);
            
            }


            foreach (MapNode node in worldMap) node.ResetVisits();
            worldMap[7, 4].SetThisAsStartingPoint();
            worldMap[0, 4].SetThisAsEndPoint();

        
        }




    }
}


