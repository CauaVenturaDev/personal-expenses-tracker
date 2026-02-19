using personalExpensesTracker.Application.DTOs;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Application.Interfaces;

public interface IExpensesServices
{
    Task<Expense> AddAsync(Expense expense);
    Task<List<Expense>> GetByMonthAsync(int month, int year);
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<CategorySumaryDto>> GetTotalByCategoryAsync(int month, int year);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(int id);
}
