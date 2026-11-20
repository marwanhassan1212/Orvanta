
namespace Orvanta.ViewModels.Categories
{
    public class CategoryListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int ProductsCount { get; set; }
    }
}
