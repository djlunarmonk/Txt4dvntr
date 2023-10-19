namespace Txt4dvntr.Classes
{
    public class Obstruction : Thing
    {
        private int x, y;
        private bool _locked {  get; set; }
        private Exits _exit;
        private string _onOpen;
        private static int _id = 0;
        public int id { get; set; }
        public Obstruction(string handle, string shorthand, string description, string abilities, ref MapNode room, string onOpen, Exits exits, string keyHandle, string keyShortHand, string keyDescription) : base(handle, shorthand, description, abilities)
        {
            _exit = exits;
            _locked = true;
            _onOpen = onOpen;
            x = Convert.ToInt32(room.id.Substring(0, 2));
            y = Convert.ToInt32(room.id.Substring(2, 2));
            this.id = _id;
            Solution solution = new Solution(keyHandle, keyShortHand, keyDescription, "1", _id);
            InterActions.SetNew(solution, this, "openObstruction");
            room.Inventory.Add(this);
            room.Inventory.Add(solution);
            _id++;
        }

        public Exits exit 
        {
            get
            {
                return _exit;
            }
            set
            { }
            
        }

        public string Display()
        {
            string message = "";
            message += $"To the {_exit} is a {ShortHand}";
            message += _locked ? ". " : ", but it seems you can pass through now. ";

            return message;
        }

        public void OnOpen()
        {
            Program.game.worldMap[y, x].Exits |= _exit;

            switch (_exit)
            {
                case Exits.north:
                    Program.game.worldMap[y-1, x].Exits |= Exits.south; break;
                case Exits.south:
                    Program.game.worldMap[y+1, x].Exits |= Exits.north; break;
                case Exits.east:
                    Program.game.worldMap[y, x+1].Exits |= Exits.west; break;
                case Exits.west:
                    Program.game.worldMap[y, x-1].Exits |= Exits.east; break;
            }
            _locked = false;
            Game.Print($"\n{_onOpen}. \n");
        }
    }
}
