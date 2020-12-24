
namespace Library.ERPReports
{
    public class Inventory
    {
        public string ProductId { get; }
        public string Location { get; }
        public int Balance { get; }             //leftover products count

        public Inventory(string id, string location, int balance)
        {
            ProductId = id;
            Location = location;
            Balance = balance;
        }
    }
}
