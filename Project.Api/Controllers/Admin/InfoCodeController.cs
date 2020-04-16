using Project.Service;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;

namespace Project.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/infocode")]
    public class InfoCodeController : BaseController<InfoCode, IInfoCodeService>
    {
        public InfoCodeController(IInfoCodeService infoCodeService, IErrorLogService errorLogService) 
            : base(infoCodeService, errorLogService)
        {

        }
    }
}