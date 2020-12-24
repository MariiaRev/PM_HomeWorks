using System.Collections.Generic;

namespace Library
{
    public class PlayerAgeComparer : IComparer<Player>
    {
        public int Compare(Player pl1, Player pl2)
        {
            if (pl1.Age > pl2.Age)
                return 1;
            if (pl1.Age < pl2.Age)
                return -1;
            else
                return 0;
        }
    }
}
