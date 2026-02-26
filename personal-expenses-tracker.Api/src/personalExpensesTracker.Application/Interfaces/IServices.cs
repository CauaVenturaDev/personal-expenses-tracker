namespace personalExpensesTracker.Application.Interfaces;

public interface IServices<T, A, B, M>
{
    Task<T> AddAsync(T t);
    Task<List<T>> GetByMonthAsync(int month, int year);
    Task<List<M>> GetAllDetailedAsync();
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<A>> GetTotalByCategoryAsync(int month, int year);
    Task<T> UpdateAsync(int id, B b);
    Task DeleteAsync(int id);
    Task DeleteAllAsync();
}
