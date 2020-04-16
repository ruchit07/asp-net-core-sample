using Project.Service;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;

namespace Project.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UsersController : BaseController<Users, IUsersService>
    {
        public UsersController(IUsersService UsersService, IErrorLogService errorLogService)
            : base(UsersService, errorLogService)
        {

        }
    }
}