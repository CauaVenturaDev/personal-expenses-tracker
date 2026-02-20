using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace personalExpensesTracker.Infrastructure;

public static class IfrastructureModule
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Configure the database connection
        services.AddDbContext<PersonalExpensesTrackerContext>(options =>
            options.UseNpgsql("Server:localhost;Port=5500;Database=personal-expenses-tracker;Username=admin;Password=admin;"));

        return services;
    }
}
