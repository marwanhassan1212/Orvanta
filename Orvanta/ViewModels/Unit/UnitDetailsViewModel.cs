
namespace Orvanta.ViewModels.Units
{
    public class UnitDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int ProductsCount { get; set; }
    }
}