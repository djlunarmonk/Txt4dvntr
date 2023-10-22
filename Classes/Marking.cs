using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txt4dvntr.Classes
{
    public class Marking : Thing
    {
        public Marking(string shorthandOneWordOnly, string description) : base(shorthandOneWordOnly, shorthandOneWordOnly, description, "0")
        {
        }

        public string Display()
        {
            return $"There is a {base.ShortHand} here saying {base.Description}. ";
        }
    }
}
