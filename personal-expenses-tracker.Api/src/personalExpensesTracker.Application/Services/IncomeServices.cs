using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Services.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Domain.NonEntity;
using personalExpensesTracker.Infrastructure.Data;

namespace personalExpensesTracker.Application.Services;

public class IncomesServices(PersonalExpensesTrackerContext context) : IIncomesServices
{

    private readonly PersonalExpensesTrackerContext _context = context;

    public Task<Income> AddAsync(Income expense)
    {
        throw new NotImplementedException();
    }

    public Task<List<Income>> DeleteAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MonthlyIncomes>> GetAllDetailed()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Income>> GetByMonth(int month, int year)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(int month, int year)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        throw new NotImplementedException();
    }

    public Task<Income> UpdateAsync(int id, IncomeCreateRequest dto)
    {
        throw new NotImplementedException();
    }
}
