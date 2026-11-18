namespace Orvanta.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseNumber { get; set; } = string.Empty;
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; } = null!;
        public DateTime ExpenseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DocumentStatus Status { get; set; }
        public string? Notes { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public string? PostedByUserId { get; set; }
        public DateTime? PostedAt { get; set; }
        public string? VoidedByUserId { get; set; }
        public DateTime? VoidedAt { get; set; }
        public string? VoidReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}