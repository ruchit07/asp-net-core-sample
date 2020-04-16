namespace Project.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity, long key);
        Task<T> UpdateAsync(T entity, int key);
        Task<T> UpdateAsync(T updated);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where);
        Task<long> DeleteAsync(T entity);
        Task<long> DeleteAsync(Expression<Func<T, bool>> where);
        IEnumerable<T> ExecuteSP<T>(string query, params SqlParameter[] SqlPrms) where T : new();
        Task TruncateAsync(String TableName);
        Task SaveAsync();
        void BeginTransaction();
        void Rollback();
        Task Commit();
    }
}