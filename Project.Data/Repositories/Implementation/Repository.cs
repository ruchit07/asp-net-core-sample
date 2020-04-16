namespace Project.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Project.Data.Context;
    using Project.Data.Infrastructure;
    using Project.Data.Helper;

    public class Repository<T> where T : class
    {
        protected ProjectContext dbContext;
        private IUnitOfWork _unitOfWork;
        public IConfiguration config;        
       
        public Repository(IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            dbContext = dbFactory.Init();
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return dbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T t)
        {
            dbContext.Set<T>().Add(t);
            await dbContext.SaveChangesAsync();
            return t;
        }

        public virtual async Task<T> UpdateAsync(T updated, long key)
        {
            if (updated == null)
                return null;

            T existing = await dbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                await dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = await dbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                await dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated)
        {
            if (updated == null)
                return null;

            dbContext.Attach(updated);
            dbContext.Entry(updated).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return updated;
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where)
        {
            return dbContext.Set<T>().Where(where);
        }

        public virtual async Task<long> DeleteAsync(T t)
        {
            dbContext.Set<T>().Remove(t);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<long> DeleteAsync(Expression<Func<T, bool>> where)
        {
            var entities = dbContext.Set<T>().Where(where);
            dbContext.Set<T>().RemoveRange(entities);
            return await dbContext.SaveChangesAsync();
        }

        public virtual IEnumerable<T> ExecuteSP<T>(string query, params SqlParameter[] SqlPrms) where T : new()
        {
            DataSet ds = new DataSet();
            string connectionString = dbContext.Database.GetDbConnection().ConnectionString;            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddRange(SqlPrms);
                    command.CommandTimeout = 0;
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            dataAdapter.SelectCommand = command;
                            dataAdapter.Fill(ds);
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

            return Utility.CreateListFromTable<T>(ds.Tables[0]);
        }

        public virtual async Task TruncateAsync(String TableName)
        {
            await dbContext.Database.ExecuteSqlCommandAsync("Truncate Table " + TableName);
        }

        public virtual async Task SaveAsync()
        {
            dbContext.SaveChanges();
            await _unitOfWork.CommitAsync();
        }

        public virtual void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public virtual void Rollback()
        {
            _unitOfWork.Rollback();
        }

        public virtual async Task Commit()
        {
            await _unitOfWork.Commit();
        }
    }
}