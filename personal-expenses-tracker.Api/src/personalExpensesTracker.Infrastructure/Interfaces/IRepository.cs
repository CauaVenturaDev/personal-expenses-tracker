using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Interfaces;

public interface IRepository<T>
{
    Task<T> AddAsync(T t);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<T>> GetByMonthAsync(int month, int year);
    Task UpdateAsync(T t);
    Task DeleteAsync(T t);
    Task<List<T>> DeleteAllAsync();
}
