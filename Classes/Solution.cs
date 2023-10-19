namespace Txt4dvntr.Classes
{
    public class Solution : Thing
    {
        public int id { get; set; }
        public Solution(string handle, string shorthand, string description, string abilities, int id) : base(handle, shorthand, description, abilities)
        {
            this.id = id;
        }
    }
}
