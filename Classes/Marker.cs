using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txt4dvntr.Classes
{
    public enum Numbers
    {
        zero = 0, one = 1, two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8, nine = 9, ten = 10, eleven = 11, twelve = 12, thirteen = 13, fourteen = 14
    }

    public class Marker : Thing
    {
        private string _loadShortHand;
        private int _capacity;
        public Marker(string handleAndShortHand, string description, string abilities, string loadShortHand, int capacity=3) : base(handleAndShortHand, handleAndShortHand, description, "1")
        {
            _loadShortHand = loadShortHand;
            _capacity = capacity;
        }

    public override void Examine()
        {
            string summary = ($"It's a {Description}, you could use it to mark a spot. (\"use {Handle}\" would be the command) ");
            if (_capacity < 1) { summary += "Too bad it's empty. "; }
            else
            { 
                summary += $"It can be used {(Numbers)_capacity} time";
                summary += _capacity > 1 ? "s. " : ". ";
            }

            Game.Print(summary);
        }

        public Marking MarkThisPlace()
        {
            Marking marking = null;
            if (_capacity < 1) { Game.Print($"You can't use an empty {Handle}. "); }
            else
            {
                Game.Print("What mark do you want to make? (15 characters tops)\n");
                Console.Write("\n> ");
                string playersMark = Console.ReadLine();
                if (playersMark is null || playersMark == "") { Game.Print("Nothing? Suit yourself. "); marking = new Marking(_loadShortHand, "\"nothing\""); }
                else
                {
                    if (playersMark.Length > 15) { playersMark = playersMark.Substring(0, 15); }
                    marking = new Marking(_loadShortHand, $"\"{playersMark}\"");
                }
                _capacity--;
                Game.Print($"You make a {marking.ShortHand} here saying {marking.Description}. ");
            }
            
            return marking;
        }
    }
}
