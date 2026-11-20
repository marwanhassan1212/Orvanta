
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Units
{
    public class UnitFilterViewModel
    {
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortDirection { get; set; } = "asc";

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set; } = 10;
    }
}
