using Project.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    public interface IInfoCodeRepository : IRepository<InfoCode>
    {
        Task<IEnumerable<InfoCodeResult>> GetAllAsync(InfoCodeFilter filter);
    }
}
