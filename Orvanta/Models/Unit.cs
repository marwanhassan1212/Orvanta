
namespace Orvanta.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
