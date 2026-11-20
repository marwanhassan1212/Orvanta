
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Products
{
    public class ProductCreateViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string SKU { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UnitId { get; set; }

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal PurchasePrice { get; set; }

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal SellingPrice { get; set; }
    }
}
