
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using personalExpensesTracker.Models;

namespace personalExpensesTracker.Data.Configurations;

public class ExpensesConfigurations : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> entity)
    {
        entity.HasKey(e => e.Id).HasName("expenses_pkey");

        entity.ToTable("expenses");

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
