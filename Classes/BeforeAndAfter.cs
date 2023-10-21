namespace Txt4dvntr.Classes
{
    public static class BeforeAndAfter
    {


        public static void Before()
        {
            // This would ask the player for their name and possibly set up something, but I will not implement this now.
        }


        public static string StartupStory()
        {
            // This should fetch something from an xml-file or a database, when I know how to.
            return "You wake up face down on a cold stone floor. Your right eyebrow stings, and as you lift your head you recognize some dry blood on the floor where your face " +
                "was planted. Your muscles are all sore, but you force yourself to stand up. Your head starts pounding and you grin. What happened here? Looking around sadly " +
                "doesn't jog your memory. Where is here?\n";
        }

        public static string StartReminder()
        {
            return "This is where you first woke up, right? Little stain there looks familiar.";
        }
        public static void After()
        {
            Console.Write("\n\n> ");
            Thread.Sleep(8000);
            Game.Print("\n\nHey, there is a boat here! It has fresh water and food, well it's just salty crackers. You eat and set sail. You finished the game, congrats pal!");
        }
    }
}