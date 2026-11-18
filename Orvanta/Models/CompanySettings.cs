namespace Orvanta.Models
{
    public class CompanySetting
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string CurrencyCode { get; set; } = "EGP";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}