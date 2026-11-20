
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Warehouses
{
    public class WarehouseCreateViewModel
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(
            @"^[a-zA-Z0-9_-]+$",
            ErrorMessage = "Code can contain only letters, numbers, hyphens and underscores.")]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }
    }
}
