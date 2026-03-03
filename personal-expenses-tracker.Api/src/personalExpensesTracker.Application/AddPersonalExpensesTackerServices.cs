using Microsoft.Extensions.DependencyInjection;
using personalExpensesTracker.Application.Services;

namespace personalExpensesTracker.Application
{
    public static class AddPersonalExpensesTackerServices
    {
        public static IServiceCollection AddPersonalExpensesServices(this IServiceCollection services
        )
        {
            services.AddScoped<IExpensesServices, ExpensesServices>();

            return services;
        }
    }
}
