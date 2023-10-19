using System.ComponentModel.Design;
using Txt4dvntr;

namespace Txt4dvntr.Classes
{
    public class MapNode
    {
        private bool _startPoint;
        private bool _endPoint;
        public string id;
        private int _visits;
        public string Description { get; set; }

        public string FirstVisitInfo { get; set; }
        public List<Thing> Inventory { get; set; }
        public Exits Exits { get; set; }



        public MapNode(string id, string description, List<Thing> inventory, Exits exits, string firstVisitInfo = "")
        {
            _startPoint = _endPoint = false;
            _visits = 0;
            this.id = id;
            Description = description;
            Inventory = inventory;
            FirstVisitInfo = firstVisitInfo;
            Exits = exits;
        }

        public MapNode(string id, string visits, string description, string exits, string firstVisitInfo = "")
        {
            _startPoint = _endPoint = false;
            _visits = Convert.ToInt32(visits);
            this.id = id;
            Description = description;
            Inventory = new List<Thing>();
            FirstVisitInfo = firstVisitInfo;
            Exits = (Exits)Convert.ToInt32(exits,2);
        }



        public void Visit()
        {

            _visits++;
            if (_visits == 1)
            {
                OnFirstVisit();
            }
            Display();
            
            if (_endPoint) { Program.game.keepPlaying = false; BeforeAndAfter.After(); }

        }

        public void ResetVisits()
        {
            _visits = 0;
        }
        public void OnFirstVisit()
        {
            if (_startPoint) { Game.Print($"{BeforeAndAfter.StartupStory()} "); }
            Game.Print(FirstVisitInfo);
            // Console.WriteLine("This is your first visit here!");
        }

        public void Display()
        {
            string coordinates = $"({Convert.ToInt32(id.Substring(0, 2))}, {Convert.ToInt32(id.Substring(2))})"; 
            string summary =  $"{Description} From here you can go {Exits}. ";

            if (Inventory.Any())
            {
                int inventoryCount = Inventory.Count();
                foreach (Thing thing in Inventory)
                {
                    if (thing is Obstruction)
                    {
                        inventoryCount--;
                        summary += (thing as Obstruction).Display();
                    }
                }
                if (_startPoint && _visits > 1) { summary += $" {BeforeAndAfter.StartReminder()} "; }

                if (inventoryCount > 0)
                {
                    summary += inventoryCount > 1 ? "Here are " : "Here is ";
                    int i = 0, counter = 0;
                    
                    while (counter < Inventory.Count)
                    {
                        if (Inventory[counter] is not Obstruction)
                        {
                            summary += $"a {Inventory[counter].ShortHand}";
                            if (i == inventoryCount - 1) { summary += ". "; }
                            else if (i == inventoryCount - 2) { summary += " and "; }
                            else { summary += ", "; }
                            i++;
                        }
                        counter++;
                    }

                }
            }

            Game.Print(summary);
            //int charNo = 0;
            //for (int i = 0; i < summary.Length; i++)
            //{
            //    char c = summary[i];
            //    charNo++;
            //    if ((charNo > _lineBreak - 10) && char.IsWhiteSpace(c))
            //    {
            //        Console.Write("\n");
            //        charNo = 0;
            //    }
            //    else if (charNo == 1 && char.IsWhiteSpace(c))
            //    {
            //        ;
            //    }
            //    else { Console.Write(c); }
            //}
            //Console.Write("\n");
        }

        public string EuroSeparatedLine()
        {
            return $"{id}€{_visits}€{Description}€{Convert.ToString((int)Exits, 2)}€{FirstVisitInfo}";
        }

        public string ID() { return id; }

        public void SetThisAsStartingPoint()
        {
            _startPoint = true;
        }

        public void SetThisAsEndPoint()
        {
            _endPoint = true;
        }
    }
}
