using System.Collections.Generic;

namespace Library
{
    public class PlayerEqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player pl1, Player pl2)
        {
            //Check whether the compared objects reference the same data
            if (ReferenceEquals(pl1, pl2))
                return true;

            //Check whether any of the compared objects is null
            if (pl1 is null || pl2 is null)
                return false;

            //Check whether the players' properties are equal
            return pl1.FirstName == pl2.FirstName 
                && pl1.LastName == pl2.LastName 
                && pl1.Age == pl2.Age 
                && pl1.Rank == pl2.Rank;
        }

        public int GetHashCode(Player player)
        {
            //Check whether the object is null
            if (player is null)
                return 0;

            //Get hash code for the FirstName field if it is not null
            int hashPlayerFirstName = player.FirstName == null ? 0 : player.FirstName.GetHashCode();
            
            //Get hash code for the LastName field if it is not null
            int hashPlayerLastName = player.LastName == null ? 0 : player.LastName.GetHashCode();

            //Get hash code for the Age field
            int hashPlayerAge = player.Age.GetHashCode();
            
            //Get hash code for the Rank field
            int hashPlayerRank = player.Rank.GetHashCode();

            //Calculate the hash code for the player
            return hashPlayerFirstName ^ hashPlayerLastName ^ hashPlayerAge ^ hashPlayerRank;
        }
    }
}
