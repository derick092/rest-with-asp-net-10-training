using RWAN10T.Api.Model;

namespace RWAN10T.Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUserName(string username);
    }
}
