using Project.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IInfoCodeService : IService<InfoCode>
    {
        Task<IEnumerable<InfoCodeResult>> GetAllAsync(InfoCodeFilter filter);
    }
}