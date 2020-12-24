
namespace Library.ERPReports
{
    public class Product
    {
        public string Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public decimal Price { get; }

        public Product(string id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }


        public override string ToString()
        {
            return $" {Id, 4} {Brand, 15} {Model, 15} {Price, 10}";
        }
    }
}
