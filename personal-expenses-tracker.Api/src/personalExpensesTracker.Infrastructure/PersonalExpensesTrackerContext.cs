
using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure;

public partial class PersonalExpensesTrackerContext : DbContext
{
    // Construtor sem parâmetros para permitir a criação do contexto sem opções
    public PersonalExpensesTrackerContext()
    {
    }


    // Construtor que aceita opções de configuração para o contexto
    public PersonalExpensesTrackerContext(DbContextOptions<PersonalExpensesTrackerContext> options)
        : base(options)
    {
    }


    // DbSet para a entidade Expense e Income, representando a tabela de despesas e receita no banco de dados
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<Income> Incomes { get; set; }


    // OnModelCreating metodo para aplicar as configurações do modelo 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalExpensesTrackerContext).Assembly);
    }
}
