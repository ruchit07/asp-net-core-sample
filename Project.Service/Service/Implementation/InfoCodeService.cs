using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Data.Repositories;
using Project.Data.Models;

namespace Project.Service
{
    public class InfoCodeService : Service<InfoCode>, IInfoCodeService
    {
        private readonly IInfoCodeRepository _infoCodeRepository;

        public InfoCodeService(IInfoCodeRepository infoCodeRepository) : base(infoCodeRepository)
        {
            _infoCodeRepository = infoCodeRepository;
        }

        public async Task<IEnumerable<InfoCodeResult>> GetAllAsync(InfoCodeFilter filter)
        {
            return await _infoCodeRepository.GetAllAsync(filter);
        }
    }
}
