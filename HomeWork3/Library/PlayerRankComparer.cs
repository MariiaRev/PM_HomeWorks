using System.Collections.Generic;

namespace Library
{
    public class PlayerRankComparer : IComparer<Player>
    {
        public int Compare(Player pl1, Player pl2)
        {
            string rank1 = pl1.Rank.ToString();
            string rank2 = pl2.Rank.ToString();

            return string.Compare(rank1, rank2);
        }
    }
}
