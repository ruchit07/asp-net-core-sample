using Project.Data.Models;
using Project.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ErrorLogService : Service<ErrorLog>, IErrorLogService
    {
        private readonly IErrorLogRepository _erorrLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository) : base(errorLogRepository)
        {
            _erorrLogRepository = errorLogRepository;
        }

        public async Task LogExceptionAsnc(Exception ex)
        { 
            await _erorrLogRepository.AddAsync(new ErrorLog(){
                CreatedTime = DateTime.UtcNow,
                InnerException = ex.InnerException.Message ?? null,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
            });
        }

        public async Task LogMessageAsync(string message)
        {
            await _erorrLogRepository.AddAsync(new ErrorLog()
            {
                CreatedTime = DateTime.UtcNow,
                Message = message
            });
        }
    }
}
