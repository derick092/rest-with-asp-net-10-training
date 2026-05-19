using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Repositories
{
    public class UserRepository(MSSQLContext context) : GenericRepository<User>(context), IUserRepository
    {
        public User? FindByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
