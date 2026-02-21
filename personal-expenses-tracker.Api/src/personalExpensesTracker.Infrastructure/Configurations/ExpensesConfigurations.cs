using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Configurations;

public class ExpensesConfigurations : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> entity)
    {
        // Chave primária 
        entity.HasKey(e => e.Id).HasName("expenses_pkey");


        // Mapeamento da tabela
        entity.ToTable("expenses");


        // Mapeamento das colunas
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
