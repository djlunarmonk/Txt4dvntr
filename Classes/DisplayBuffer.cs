namespace Txt4dvntr.Classes
{
    public static class DisplayBuffer
    {
        private static Queue<string> actionBuffer = new Queue<string>();

        public static void Add(string input)
        {
            actionBuffer.Enqueue(input);
        }

        public static void Clear()
        {
            actionBuffer.Clear();
        }

        public static void Show()
        {
            foreach (var item in actionBuffer)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        #region old bullshit
        //static List<string> wholeScreen = new List<string>();
        //static List<string> screenTop = new List<string>();
        //static Queue<string> screenBottom = new Queue<string>();


        //public static List<string> GetDisplayLines()
        //{
        //    wholeScreen.Clear();
        //    int remainder = 23 - screenTop.Count;
        //    if (screenTop.Count != 0)
        //    {
        //        foreach (string line in screenTop)
        //        {
        //            wholeScreen.Add(line);
        //        } 
        //    }
        //    wholeScreen.Add("\n");

        //    if (remainder >= screenBottom.Count)
        //    {
        //        foreach (string line in screenBottom)
        //        {
        //            wholeScreen.Add(line);
        //        }
        //        for (int i = 0; i < (remainder - screenBottom.Count); i++)
        //        {
        //            wholeScreen.Add("\n");
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < remainder; i++)
        //        {
        //            wholeScreen.Add(screenBottom.Dequeue());
        //        }
        //    }
        //return wholeScreen;
        //}

        //public static void AddAction(string action)
        //{
        //    screenBottom.Enqueue(action);
        //}

        //public static void SetScreenTop(string addition)
        //{
        //    if (screenTop.Count < 12)
        //    {
        //        screenTop.Add(addition);
        //    }
        //}
        #endregion

    }
}
