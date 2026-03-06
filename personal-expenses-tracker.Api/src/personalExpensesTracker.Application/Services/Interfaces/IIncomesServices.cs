using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Domain.Entity.Models;
using personalExpensesTracker.Domain.NonEntity;

namespace personalExpensesTracker.Application.Services.Interfaces
{
    public interface IIncomesServices
    {
        Task<Income> AddAsync(
            Income expense
            );

        Task<IEnumerable<MonthlyIncomes>> GetAllDetailed();

        Task<IEnumerable<Income>> GetByMonth(
            int month,
            int year
            );

        Task<IEnumerable<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(
            int month,
            int year
            );

        Task<decimal> GetTotalByMonthAsync(
            int month,
            int year
            );

        Task<Income> UpdateAsync(
            int id,
            IncomeCreateRequest dto
            );

        Task DeleteAsync(
            int id
            );

        Task<List<Income>> DeleteAllAsync();
    }
}