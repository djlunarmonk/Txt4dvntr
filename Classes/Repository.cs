namespace Txt4dvntr.Classes
{
    public static class Repository
    {
        

        public static MapNode[,] GetWorldMap(int x, int y)
        {
            MapNode[,] worldMap = new MapNode[y, x];

            
            for (int i = 0; i < worldMap.GetLength(0); i++)
            {
                for (int j = 0; j < worldMap.GetLength(1); j++)
                {
                    // Making the id from current coordinates
                    string id = $"{j.ToString("x2")}{i.ToString("x2")}";


                    // Making a generic description and switching on exits
                    string description = "This is sandy grassland. ";
                    Exits exits = new Exits();
                    
                    switch (i)
                    {
                        case 0:
                            exits |= Exits.south;
                            description += "This is the north shore. ";
                            break;
                        case 1:
                            exits |= Exits.south | Exits.north;
                            if (j == 1)
                            {
                                description += "No water here. ";
                            }
                            break;

                        case 2:
                            exits |= Exits.north;
                            description += "This is the south shore. ";
                            break;
                    }
                    switch (j)
                    {
                        case 0:
                            exits |= Exits.east;
                            description += "This is the west shore. ";
                            break;
                        case 1:
                            exits |= Exits.west | Exits.east;
                            break;
                        case 2:
                            exits |= Exits.west;
                            description += "This is the east shore. ";
                            break;
                    }

                    Abilities tempAblys = new Abilities();
                    
                    List<Thing> inventory = new List<Thing>();
                    if (!(i == 1 && j == 1) && (i+j!=4))
                    {
                        tempAblys |= Abilities.WillTravel;

                        inventory.Add(new Thing("thing", "small thing", "strange little thing", Convert.ToString((int)tempAblys, 2)));
                    }
                    if ((i + j == 4))
                    {
                        tempAblys |= Abilities.None;

                        inventory.Add(new Thing("nose", "small nose", "snotty little nose", Convert.ToString((int)tempAblys, 2)));
                    }

                    worldMap[i,j] = new MapNode(id, description, inventory, exits);
                }
            }


            Obstruction door = new Obstruction("door", "large wooden door",
                                                $"large wooden door in a rustique style, with black metal details but no ingravings. It has a keyhole.",
                                                "0", ref worldMap[1, 1], "The door opens", Exits.north,
                                                "key", "rusty key", "rusty key, nothing special about it");
            Thing key = worldMap[1, 1].Inventory.Where(t => t.Handle == "key").SingleOrDefault();
            if (key is not null)
            {
                Container outerDoll = new Container("doll", "Russian doll", "wooden doll painted in beautiful, traditional decor", "1", 1);
                Container innerDoll = new Container("doll", "smaller Russian doll", "small wooden doll painted in beautiful, traditional decor", "1", 1);
                innerDoll.Inventory.Add(key);
                InterActions.SetNew(key,door, "openObstruction");
                worldMap[1,1].Inventory.Remove(key);
                outerDoll.Inventory.Add(innerDoll);
                worldMap[1, 1].Inventory.Add(outerDoll);
                worldMap[1, 1].SetThisAsStartingPoint();
                worldMap[0, 1].SetThisAsEndPoint();
            }   
            


            return worldMap;
        }




    }
}
