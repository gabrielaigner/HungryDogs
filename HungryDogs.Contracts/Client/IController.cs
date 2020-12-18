using System;
using System.Threading.Tasks;

namespace HungryDogs.Contracts.Client
{
    public interface IController<T> : IDisposable
    {
        Task<int> Count();
        Task<T> CreateAsync();
        Task<T> DeleteAsync(int id);
        Task<T[]> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}