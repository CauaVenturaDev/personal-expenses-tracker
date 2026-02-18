using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using personalExpensesTracker.Models;

namespace personalExpensesTracker.Data.Configurations;

public class IncomeConfigurations : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> entity)
    {
        entity.HasKey(e => e.Id).HasName("income_pkey");

        entity.ToTable("income");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Amount)
            .HasPrecision(10, 2)
            .HasColumnName("amount");
        entity.Property(e => e.Category)
            .HasMaxLength(255)
            .HasColumnName("category");
        entity.Property(e => e.Date).HasColumnName("date");
        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("description");
    }
}
