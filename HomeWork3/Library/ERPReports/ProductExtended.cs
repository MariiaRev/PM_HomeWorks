using System.Collections.Generic;

namespace Library.ERPReports
{
    public class ProductExtended : Product
    {
        public string Location { get; }                         //pair Id - Location is a composite key
        public int Balance { get; }
        public List<string> TagsValue { get; }

        public ProductExtended(string id, string location, int balance, string brand, string model, decimal price, List<string> tags)
            :base(id, brand, model, price)
        {
            Location = location;
            Balance = balance;
            TagsValue = tags;
        }


        public override string ToString()
        {
            string tags = "[";
            
            foreach(var tag in TagsValue)
            {
                tags += tag + ", ";
            }

            tags = tags.Remove(tags.Length - 2) + "]";


            //return base.ToString() + $" {tags, 30}";
            return base.ToString() + $" {tags, 28}";
        }
    }
}
