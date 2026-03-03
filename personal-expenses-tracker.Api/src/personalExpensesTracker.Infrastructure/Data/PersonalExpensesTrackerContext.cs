
using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Data;

public partial class PersonalExpensesTrackerContext : DbContext
{
    public PersonalExpensesTrackerContext()
    {
    }

    public PersonalExpensesTrackerContext(DbContextOptions<PersonalExpensesTrackerContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<Income> Incomes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalExpensesTrackerContext).Assembly);

    }
}
