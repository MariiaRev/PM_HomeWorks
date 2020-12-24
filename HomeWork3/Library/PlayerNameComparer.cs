using System.Collections.Generic;

namespace Library
{
    public class PlayerNameComparer: IComparer<Player>
    {
        public int Compare(Player pl1, Player pl2)
        {
            string name1 = pl1.LastName + " " + pl1.FirstName;
            string name2 = pl2.LastName + " " + pl2.FirstName;

            return string.Compare(name1, name2);
        }
    }
}
