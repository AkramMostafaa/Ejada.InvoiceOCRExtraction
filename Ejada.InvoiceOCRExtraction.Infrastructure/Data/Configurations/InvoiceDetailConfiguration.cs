using Ejada.InvoiceOCRExtraction.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ejada.InvoiceOCRExtraction.Infrastructure.Data.Configurations;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {


        builder.Property(d => d.Description)
            .IsRequired();

        builder.Property(d => d.Quantity)
            .IsRequired();

        builder.Property(d => d.UnitPrice)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(d => d.LineTotal)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(d => d.InvoiceId)
            .IsRequired();
    }
}
