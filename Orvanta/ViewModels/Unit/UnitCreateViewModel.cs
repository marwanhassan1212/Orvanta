using System.ComponentModel.DataAnnotations;
namespace Orvanta.ViewModels.Units
{
    public class UnitCreateViewModel
    {
        [Required(ErrorMessage = "Unit name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unit symbol is required.")]
        [StringLength(20, MinimumLength = 1)]
        public string Symbol { get; set; } = string.Empty;
    }
}