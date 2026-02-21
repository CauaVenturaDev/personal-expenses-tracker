using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Interfaces;

public interface IIncomeRepository
{
        Task<Income> AddIncomeAsync(Income income);
        Task<Income?> GetIncomeByIdAsync(int id);
        Task<decimal> GetTotalByMonthAsync(int month, int year);
        Task<List<Income>> GetIncomesByMonthAsync(int month, int year);
        Task UpdateIncomeAsync(Income income);
        Task DeleteIncomeAsync(Income income);
    
}
