
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Categories
{
    public class CategoryUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
