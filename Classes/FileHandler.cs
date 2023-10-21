using System.Reflection;

namespace Txt4dvntr.Classes
{
    public static class FileHandler
    {
        //private static string dirName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //private static string projectName = @"Txt4dvntr\";
        //private static int pos = dirName.IndexOf(projectName) + projectName.Length;
        private static string mapFilename = Path.Join(GetDirectoryPath(), "Map01", "WorldMap.txt");
        //private static string thingsFilename = dirName.Substring(0, pos) + @"\Map01\WorldlyThings.txt";
        //private static string playerFilename = dirName.Substring(0, pos) + @"\Map01\WorldPlayer.txt";

        public static void SaveGame(ref MapNode[,] worldMap, ref Player player)
        {
            // saving all locations and their descriptions
            using (StreamWriter writer = new StreamWriter(mapFilename))
            {
                foreach (MapNode node in worldMap) { writer.WriteLine(node.EuroSeparatedLine()); }
            }

            #region unused for now: saving things and player
            //// saving all things in the world
            //using (StreamWriter writer = new StreamWriter(thingsFilename))
            //{
            //    foreach (MapNode node in worldMap)
            //    {
            //        foreach (Thing thing in node.Inventory) { writer.WriteLine($"{node.ID()}{thing.EuroSeparatedLine()}"); }
            //    }
            //}

            //// saving the players current info
            //using (StreamWriter writer = new StreamWriter(playerFilename))
            //{
            //    writer.WriteLine(player.EuroSeparatedLine());

            //    foreach (Thing thing in player.Inventory) { writer.WriteLine(thing.EuroSeparatedLine()); }
            //}
            #endregion
        }

        private static string GetDirectoryPath()
        {
            return Environment.CurrentDirectory;
        }
        
        public static void ReviveMap(ref MapNode[,] worldMap)
        {
            using (StreamReader reader = new StreamReader(mapFilename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cutupESV = line.Split('€');
                    int x = Convert.ToInt32(cutupESV[0].Substring(0, 2));
                    int y = Convert.ToInt32(cutupESV[0].Substring(2, 2));
                    if (cutupESV.Length == 5)
                    {
                        try
                        {
                            worldMap[y, x] = new MapNode(cutupESV[0], cutupESV[1], cutupESV[2], cutupESV[3], cutupESV[4]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"MapESV Error: {e.Message}");
                            throw new Exception();
                        }
                    }

                }
            }
        }

    }
}
