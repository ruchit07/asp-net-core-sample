namespace Project.Data.Infrastructure
{
    using System.Threading.Tasks;
    public interface IUnitOfWork
    {
        Task Commit();
        Task CommitAsync();
        void BeginTransaction();
        void Rollback();
    }
}