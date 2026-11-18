using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orvanta.Models;

namespace Orvanta.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryStock> InventoryStocks { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<InvoiceApprovalHistory> InvoiceApprovalHistory { get; set; }
        public DbSet<CustomerReceipt> CustomerReceipts { get; set; }
        public DbSet<ReceiptAllocation> ReceiptAllocations { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<PaymentAllocation> PaymentAllocations { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<CompanySetting> CompanySettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Sales Invoice - Created By
            builder.Entity<SalesInvoice>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.CreatedSalesInvoices)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sales Invoice - Approved By
            builder.Entity<SalesInvoice>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.ApprovedSalesInvoices)
                .HasForeignKey(x => x.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Customer Receipt - Created By
            builder.Entity<CustomerReceipt>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.CreatedCustomerReceipts)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer Receipt - Posted By
            builder.Entity<CustomerReceipt>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.PostedCustomerReceipts)
                .HasForeignKey(x => x.PostedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer Receipt - Voided By
            builder.Entity<CustomerReceipt>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.VoidedCustomerReceipts)
                .HasForeignKey(x => x.VoidedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Expense - Created By
            builder.Entity<Expense>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.CreatedExpenses)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense - Posted By
            builder.Entity<Expense>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.PostedExpenses)
                .HasForeignKey(x => x.PostedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense - Voided By
            builder.Entity<Expense>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.VoidedExpenses)
                .HasForeignKey(x => x.VoidedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Supplier Payment - Created By
            builder.Entity<SupplierPayment>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.CreatedSupplierPayments)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier Payment - Posted By
            builder.Entity<SupplierPayment>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.PostedSupplierPayments)
                .HasForeignKey(x => x.PostedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier Payment - Voided By
            builder.Entity<SupplierPayment>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.VoidedSupplierPayments)
                .HasForeignKey(x => x.VoidedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Purchase Invoice - Created By
            builder.Entity<PurchaseInvoice>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.CreatedPurchaseInvoices)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Purchase Invoice - Approved By
            builder.Entity<PurchaseInvoice>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.ApprovedPurchaseInvoices)
                .HasForeignKey(x => x.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InventoryTransaction>()
            .HasOne<ApplicationUser>()
            .WithMany(u => u.InventoryTransactions)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}