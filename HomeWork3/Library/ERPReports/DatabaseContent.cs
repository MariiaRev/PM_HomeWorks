using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.ERPReports
{
    public static class DatabaseContent
    {
        public static List<Product> GetProducts(string path)
        {
            return  (from line in File.ReadLines(path).Skip(1)
                    let splited = line.Split(';')
                    select new Product
                    (
                        splited[0],
                        splited[1],
                        splited[2],
                        decimal.Parse(splited[3])
                    )).ToList();
        }

        public static List<Tag> GetTags(string path)
        {
            return (from line in File.ReadLines(path).Skip(1)
                    let splited = line.Split(';')
                    select new Tag
                    (
                        splited[0],
                        splited[1]
                    )).ToList();
        }

        public static List<Inventory> GetInventory(string path)
        {
            return (from line in File.ReadLines(path).Skip(1)
                    let splited = line.Split(';')
                    select new Inventory
                    (
                        splited[0],
                        splited[1],
                        int.Parse(splited[2])
                    )).ToList();
        }
    }
}
