using System;
using System.Linq;
using System.Collections.Generic;
using Library.ERPReports;

namespace ERPReportsBot
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                string userChoice;
                DataStorage dataStorage = new DataStorage("Products.csv", "Tags.csv", "Inventory.csv");

                Console.WriteLine("\n\n\nMenu:\n1. Exit\n2. Products\n3. Leftover products");
                Console.WriteLine("\n\nSelect menu item, please:");
                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        {
                            //exit
                            Console.WriteLine();
                            return;
                        }
                    case "2":
                        {
                            //products
                            ProductsSubmenu(dataStorage);
                        }; break;

                    case "3":
                        {
                            //leftovers
                            LeftoversSubmenu(dataStorage);

                        }; break;
                    default: Console.WriteLine("\n\nThere is no such command. Try again."); break;

                }
            }
        }

        static void ProductsSubmenu(DataStorage dataStorage)
        {
            while (true)            //stay at the submenu while 'exit-to-the-main-menu' command hasn't called
            {
                Console.WriteLine("\n\n\nSubmenu:\n1. Exit to the main menu\n2. Search product\n" +
                                   "3. Products list (ascending price)\n4. Products list (descending price)");
                Console.WriteLine("\n\nSelect submenu item, please:");
                
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        {
                            //exit to the main menu
                            return;
                        }
                    case "2":
                        {
                            //accept search string from user
                            string searchStr = AcceptStringDataFromUser("\n\nWhat do you search for? ",
                                "\nSearch query cannot be empty! Try again: ");

                            //search product
                            var searchResult = SearchProduct(dataStorage, searchStr);

                            //print results
                            Console.WriteLine($"\n\n ------------------------------------ SEARCH RESULTS ------------------------------------\n");

                            if (searchResult.Count() == 0)
                                Console.WriteLine($"{"", 22}There is no product matching the search query.");
                            else
                            {
                                Console.WriteLine($" {"ID",4} {"BRAND",15} {"MODEL",15} {"PRICE",10} {"TAGS",19}\n");

                                foreach (var prod in searchResult)
                                {
                                    Console.WriteLine(prod);
                                }
                            }
                            
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    case "3":
                        {
                            //Products list (ascending price)
                            var sortedResult = ProductsSortedByPrice(dataStorage);

                            //print results
                            Console.WriteLine($"\n\n ------------------------------------ SORTED PRODUCTS -----------------------------------\n");
                            Console.WriteLine($" {"ID",4} {"BRAND",15} {"MODEL",15} {"PRICE",10} {"TAGS",19}\n");

                            foreach (var prod in sortedResult)
                            {
                                Console.WriteLine(prod);
                            }
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    case "4":
                        {
                            //Products list(descending price)
                            var sortedResult = ProductsSortedByPrice(dataStorage, true);

                            //print results
                            Console.WriteLine($"\n\n ------------------------------------ SORTED PRODUCTS -----------------------------------\n");
                            Console.WriteLine($" {"ID",4} {"BRAND",15} {"MODEL",15} {"PRICE",10} {"TAGS",19}\n");

                            foreach (var prod in sortedResult)
                            {
                                Console.WriteLine(prod);
                            }
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    default: Console.WriteLine("\n\nThere is no such command. Try again."); break;
                }
            }
        }

        static void LeftoversSubmenu(DataStorage dataStorage)
        {
            while (true)            //stay at the submenu while 'exit-to-the-main-menu' command hasn't called
            {
                Console.WriteLine("\n\n\nSubmenu:\n1. Exit to the main menu\n2. Missing products\n" +
                                   "3. Leftovers list (ascending amount)\n4. Leftovers list (descending amount)\n5. Leftovers list (id)");
                Console.WriteLine("\n\nSelect submenu item, please:");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        {
                            //exit to the main menu
                            return;
                        }
                    case "2":
                        {
                            //missing products
                            var missingProducts = MissingProducts(dataStorage);

                            Console.WriteLine($"\n\n ----------------------------------- MISSING PRODUCTS -----------------------------------\n");
                            Console.WriteLine($" {"ID", 4} {"BRAND", 15} {"MODEL", 15} {"PRICE", 10} {"TAGS", 19}\n");
                            foreach (var prod in missingProducts)
                            {
                                Console.WriteLine(prod);
                            }
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    case "3":
                        {
                            //Leftovers list (ascending price)
                            var sortedResult = LeftoversSortedByAmount(dataStorage);

                            Console.WriteLine($"\n\n ----------------------------------- SORTED LEFTOVERS -----------------------------------\n");
                            Console.WriteLine($" {"ID", 4} {"BRAND", 15} {"MODEL", 15} {"PRICE", 10} {"TAGS", 19}{"", 10}{"BALANCE", 9}\n");

                            foreach (var prod in sortedResult)
                            {
                                Console.WriteLine($"{prod} {prod.Balance, 9}");
                            }
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    case "4":
                        {
                            //Leftovers list (descending price)
                            var sortedResult = LeftoversSortedByAmount(dataStorage, true);

                            Console.WriteLine($"\n\n ----------------------------------- SORTED LEFTOVERS -----------------------------------\n");
                            Console.WriteLine($" {"ID", 4} {"BRAND", 15} {"MODEL", 15} {"PRICE", 10} {"TAGS", 19}{"", 10}{"BALANCE", 9}\n");
                           
                            foreach (var prod in sortedResult)
                            {
                                Console.WriteLine($"{prod} {prod.Balance, 9}");
                            }
                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    case "5":
                        {
                            //accept search string from user
                            string searchStr = AcceptStringDataFromUser("\n\nEnter the product id, please: ",
                                "\nId cannot be empty! Try again: ");

                            //Leftovers list (id)
                            var leftovers = SearchProductLeftoversById(dataStorage, searchStr);

                            Console.WriteLine($"\n\n ----------------------------------- PRODUCT LEFTOVERS ----------------------------------\n");

                            if (leftovers == null)
                            {
                                Console.WriteLine($"{"", 30}There is no product with id {searchStr}");
                            }
                            else
                            {
                                if (leftovers[0].Location == null)
                                {
                                    Console.WriteLine($"{"", 25}The product with id {searchStr} is out of stocks.");
                                }
                                else
                                {
                                    Console.WriteLine($"{"", 28}{"LOCATION", -20} {"BALANCE", 10}\n");

                                    foreach (var inv in leftovers)
                                    {
                                        Console.WriteLine($"{"", 28}{inv.Location, -20} {inv.Balance, 10}");
                                    }
                                }
                            }

                            Console.WriteLine($"\n ----------------------------------------------------------------------------------------\n");

                        }; break;
                    default: Console.WriteLine("\n\nThere is no such command. Try again."); break;
                }
            }
        }

        static List<ProductExtended> SearchProduct(DataStorage dataStorage, string searchStr)
        {
            if (!dataStorage.IsDataLoaded)
            {
                dataStorage.LoadData();
            }

            //join products and their tags
            var joinedProductsTags = Join(dataStorage.Products, dataStorage.Tags, null);


            searchStr = searchStr.ToLower();            //ingnore case

            var firstPart = joinedProductsTags.Where(pr => pr.Id.ToLower() == searchStr).ToList();
            var secondPart = joinedProductsTags.Where(pr => pr.Brand.ToLower().Contains(searchStr) || pr.Model.ToLower().Contains(searchStr)).ToList();
            var thirdPart = joinedProductsTags.Where(pr => pr.TagsValue.Where(t => t.Contains(searchStr)).Any()).ToList();

            return firstPart.Concat(secondPart).Concat(thirdPart)
                            .Distinct(new ProductExtendedEqualityComparer()).ToList();
        }

        static List<ProductExtended> ProductsSortedByPrice(DataStorage dataStorage, bool descendingSort = false)
        {
            if (!dataStorage.IsDataLoaded)
            {
                dataStorage.LoadData();
            }

            //join products and their tags
            var joinedProductsTags = Join(dataStorage.Products, dataStorage.Tags, null);

            if (descendingSort)
            {
                return joinedProductsTags.OrderByDescending(pr => pr.Price).ToList();
            }
            else
            {
                return joinedProductsTags.OrderBy(pr => pr.Price).ToList();
            }
        }

        static List<ProductExtended> MissingProducts(DataStorage dataStorage)
        {
            if (!dataStorage.IsDataLoaded)
            {
                dataStorage.LoadData();
            }

            //join products with their tags and inventory
            var joinedProductsTagsInventory = Join(dataStorage.Products, dataStorage.Tags, dataStorage.Inventory);

            //select missing products
            var missingProducts = joinedProductsTagsInventory.Where(pr => pr.Balance == 0).OrderBy(pr => pr.Id).ToList();

            return missingProducts;
        }

        static List<ProductExtended> LeftoversSortedByAmount(DataStorage dataStorage, bool descendingSort = false)
        {
            if (!dataStorage.IsDataLoaded)
            {
                dataStorage.LoadData();
            }

            //join products with their tags and inventory
            var joinedProductsTagsInventory = Join(dataStorage.Products, dataStorage.Tags, dataStorage.Inventory);

            //sort
            var leftovers = from pr in joinedProductsTagsInventory
                            group pr by pr.Id into gr
                            let prod = gr.FirstOrDefault()
                            select new ProductExtended
                            (
                                gr.Key,
                                null,
                                gr.Sum(p => p.Balance),
                                prod.Brand,
                                prod.Model,
                                prod.Price,
                                prod.TagsValue
                            );

            if (descendingSort)
            {
                return leftovers.OrderByDescending(pr => pr.Balance).ToList();
            }
            else
            {
                return leftovers.OrderBy(pr => pr.Balance).ToList();
            }
        }

        static List<Inventory> SearchProductLeftoversById(DataStorage dataStorage, string idToSearch)
        {
            if (!dataStorage.IsDataLoaded)
            {
                dataStorage.LoadData();
            }

            var inventory = dataStorage.Inventory.Where(inv => inv.ProductId.ToLower() == idToSearch.ToLower())
                .OrderByDescending(inv => inv.Balance).ToList();

            if (!inventory.Any())
            {
                var searchResult = dataStorage.Products.Where(pr => pr.Id.ToLower() == idToSearch.ToLower()).FirstOrDefault();

                if (searchResult == null)                               //product was not found
                    return null;
                else                                                    //product leftovers equals 0
                    return new List<Inventory>() { new Inventory(idToSearch, null, 0) };
            }

            return inventory;

        }

        static List<ProductExtended> Join(List<Product> products, List<Tag> tags, List<Inventory> inventory)
        {
            if (tags != null)
            {
                //match products with all their tags with lowercase tag values
                var matchProductsTags = tags.GroupBy(t => t.ProductId).Select(gr => new 
                                                                        {
                                                                            Id = gr.FirstOrDefault().ProductId,
                                                                            TagsValue = gr.Select(t => t.Value.ToLower()).ToList()
                                                                        });

                var joinedProductsTags = (from pr in products
                                          join t in matchProductsTags
                                          on pr.Id equals t.Id into joined
                                          select new ProductExtended
                                          (
                                              pr.Id,
                                              null,
                                              0,
                                              pr.Brand,
                                              pr.Model,
                                              pr.Price,
                                              joined.FirstOrDefault()?.TagsValue ?? new List<string>() { "" }
                                          )
                                         ).ToList();

                if (inventory != null)
                {
                    //join joined products and tags with inventory
                    //but if inventory don't include product, include this product with empty inventory's fields
                    var joinedProductsExtended = (from pr in joinedProductsTags
                                                  join inv in inventory
                                                  on pr.Id equals inv.ProductId into joined
                                                  from j in joined.DefaultIfEmpty()
                                                  select new ProductExtended
                                                  (
                                                      pr.Id,
                                                      j?.Location ?? null,
                                                      j?.Balance ?? 0,
                                                      pr.Brand,
                                                      pr.Model,
                                                      pr.Price,
                                                      pr.TagsValue
                                                  )).ToList();

                    return joinedProductsExtended;
                }

                return joinedProductsTags;
            }
            else
                return null;
        }

        static string AcceptStringDataFromUser(string message, string messageIfEmptyInput)
        {
            Console.Write(message);
            string userData = Console.ReadLine().Trim();
            Console.WriteLine();

            if (messageIfEmptyInput != null)
            {
                while (userData == "")
                {
                    Console.Write(messageIfEmptyInput);
                    userData = Console.ReadLine().Trim();
                    Console.WriteLine();
                }
            }

            return userData;
        }

    }
}
