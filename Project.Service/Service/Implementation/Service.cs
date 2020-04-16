using Project.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service
{
    public class Service<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual Task<T> GetAsync(long id)
        {
            return _repository.GetAsync(id);
        }

        public virtual Task<T> GetAsync(int id)
        {
            return _repository.GetAsync(id);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public virtual Task<T> AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public virtual Task<T> UpdateAsync(T entity, long id)
        {
            return _repository.UpdateAsync(entity, id);
        }

        public virtual Task<T> UpdateAsync(T entity, int id)
        {
            return _repository.UpdateAsync(entity, id);
        }

        public virtual Task<T> UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where)
        {
            return await _repository.Where(where);
        }

        public virtual Task<long> DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }
    }
}
