using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Infrastructure.Configurations
{
    public class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            // Chave primária
            entity.HasKey(c => c.Id).HasName("clients_pkey");

            // Mapeamento da tabela
            entity.ToTable("clients");

            // Colunas
            entity.Property(c => c.Id)
                .HasColumnName("id");

            entity.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(c => c.Email)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("email");

            entity.Property(c => c.Password)
                .IsRequired()
                .HasColumnName("password");

            entity.Property(c => c.Username)
                .HasColumnName("username")
                .HasMaxLength(255);

            entity.Ignore(c => c.Expenses);

            entity.Ignore(c => c.Incomes); 
        }
    }
}
