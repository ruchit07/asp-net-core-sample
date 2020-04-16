using Project.Data.Repositories;
using Project.Data.Models;

namespace Project.Service
{
    public class UsersService : Service<Users>, IUsersService
    {
        public UsersService(IUsersRepository UserRepository) : base(UserRepository)
        {
        }
    }
}