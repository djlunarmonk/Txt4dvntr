using Txt4dvntr.Classes;

namespace Txt4dvntr
{

    [Flags]
    public enum Exits
    {
        nowhere = 0,   // Represents staying
        north = 1,
        northeast = 2,
        east = 4,
        southeast = 8,
        inside = 16,
        south = 32,
        southwest = 64,
        west = 128,
        northwest = 256
    }
    
    public enum LockState
    {
        open = 0,
        locked = 1
    }

    public class Program
    {
        public static Game game { get; set; }
        static void Main(string[] args)
        {

            
            #region old bullshit
            //bool keepPlaying = true;

            //for (int i = 0; i < 11; i++)
            //{
            //    GameDisplay.SetScreenTop("This is a generic description of a room full of bearded ladies who are all about to jump you with drawn knives.");
            //}
            //int counter = 60;
            //Random random = new Random();

            //// Keeping the mainwindow open and rolling
            //int elizaNo = 1;
            //do
            //{
            //    Thread.Sleep(1000);
            //    Console.Clear();

            //    foreach (string line in GameDisplay.GetDisplayLines())
            //    {
            //        Console.WriteLine(line);
            //    }

            //    for (int i = 0; i < 10; i++)
            //    {
            //        if (random.Next(1, 61) % 5 == 0)
            //        {
            //            GameDisplay.AddAction($"Eliza says hello, how are you today? {elizaNo++}");
            //        }
            //    }
            //    keepPlaying = counter-- > 0 ? true : false;


            //} while (keepPlaying);
            #endregion

            #region new bullshit
            game = new Game();
            game.RunGame();

            #endregion

            
        }

    }
}