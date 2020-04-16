using Project.Data.Models;
using System;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IErrorLogService : IService<ErrorLog>
    {
        Task LogExceptionAsnc(Exception ex);
        Task LogMessageAsync(string message);
    }
}
