using System.Linq;
using System.Collections.Generic;

namespace Library.ERPReports
{
    public class DataStorage
    {
        string ProductsPath { get; }
        string TagsPath { get; }
        string InventoryPath { get; }
        public List<Product> Products { get; private set; }
        public List<Tag> Tags { get; private set; }
        public List<Inventory> Inventory {get; private set; }
        public bool IsDataLoaded { get; private set; }

        public DataStorage(string productsPath, string tagsPath, string inventoryPath)
        {
            IsDataLoaded = false;
            ProductsPath = productsPath;
            TagsPath = tagsPath;
            InventoryPath = inventoryPath;
        }

        public void LoadData()
        {
            
            Products = DatabaseContent.GetProducts(ProductsPath);

            if (!Products.Any())
                throw new ProductException("Products list is empty.");

            Tags = DatabaseContent.GetTags(TagsPath);
            Inventory = DatabaseContent.GetInventory(InventoryPath);

            IsDataLoaded = true;
        }
    }
}
