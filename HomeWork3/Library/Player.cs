
namespace Library
{
    public class Player : IPlayer
    {
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }

        public Player (string firstName, string lastName, int age, PlayerRank rank)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{"", 11}{FirstName, -15} {LastName, -15} {Age, -7} {Rank, -15}";
        }
    }
}
