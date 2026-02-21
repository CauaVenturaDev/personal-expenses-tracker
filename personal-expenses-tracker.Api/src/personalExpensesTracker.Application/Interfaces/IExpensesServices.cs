using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Application.Interfaces;

public interface IExpensesServices
{
    Task<Expense> AddAsync(Expense expense);
    Task<List<Expense>> GetByMonthAsync(int month, int year);
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<CategorySumaryExpenseDto>> GetTotalByCategoryAsync(int month, int year);
    Task<Expense> UpdateAsync(int id, ExpenseCreateDTO expenseCreateDTO);
    Task DeleteAsync(int id);
    Task DeleteAllAsync();
}
