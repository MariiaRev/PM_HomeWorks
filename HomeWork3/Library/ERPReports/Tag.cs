
namespace Library.ERPReports
{
    public class Tag
    {
        public string ProductId { get; }
        public string Value { get; }

        public Tag(string id, string tagVal)
        {
            ProductId = id;
            Value = tagVal;
        }

        public override string ToString()       //not necessary?
        {
            return $"{ProductId} {Value}";
        }
    }
}
