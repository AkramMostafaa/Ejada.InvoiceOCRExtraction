using Ejada.InvoiceOCRExtraction.Domain.Entities;
using Ejada.InvoiceOCRExtraction.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ejada.InvoiceOCRExtraction.Infrastructure.Data;

public class InvoiceOCRDbContext: DbContext
{
    public InvoiceOCRDbContext(DbContextOptions<InvoiceOCRDbContext> options) 
        : base(options) { }

    public DbSet<Invoice> Invoices { get; set; } 
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

