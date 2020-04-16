using Project.Data.Infrastructure;
using Project.Data.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    public class InfoCodeRepository : Repository<InfoCode>, IInfoCodeRepository
    {
        public InfoCodeRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }

        public async Task<IEnumerable<InfoCodeResult>> GetAllAsync(InfoCodeFilter filter)
        {
            SqlParameter[] param = {
                        new SqlParameter("@Code", filter.Code),
                        };

            return ExecuteSP<InfoCodeResult>("GetInfoCode", param).ToList();
        }
    }
}
