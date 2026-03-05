using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Domain.NonEntity;

namespace personalExpensesTracker.Application.Services
{
    public interface IExpensesServices
    {
        Task<Expense> AddAsync(Expense expense);

        Task<IEnumerable<MonthlyExpenses>> GetAllDetailed();

        Task<IEnumerable<Expense>> GetByMonth(int month, int year);

        Task<IEnumerable<CategorySumaryExpenseDto>> GetTotalByCategoryAsync(int month, int year);

        Task<decimal> GetTotalByMonthAsync(int month, int year);
        Task<Expense> UpdateAsync(int id, ExpenseCreateRequest dto);

        Task DeleteAsync(int id);

        Task<List<Expense>> DeleteAllAsync();
    }
}