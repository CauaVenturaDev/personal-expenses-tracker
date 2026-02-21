using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Application.Interfaces;

public interface IIncomeServices
{
    Task<Income> AddAsync(Income income);
    Task<List<Income>> GetAllIncomesAsync();
    Task<List<Income>> GetByMonthAsync(int month, int year);
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(int month, int year);
    Task<Income> UpdateAsync(int id, IncomeCreateDTO incomeCreateDTO);
    Task DeleteAsync(int id);
    Task DeleteAllAsync();
}
