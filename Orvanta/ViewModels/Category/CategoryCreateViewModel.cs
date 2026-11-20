
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Categories
{
    public class CategoryCreateViewModel
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
