using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IService<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity, long id);
        Task<T> UpdateAsync(T entity, int id);
        Task<T> UpdateAsync(T updated);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where);
        Task<long> DeleteAsync(T entity);
    }
}
