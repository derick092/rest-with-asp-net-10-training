using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;

namespace RWAN10T.Api.Services
{
    public interface IUserAuthService
    {
        User? FindByUserName(string username);
        User Create(AccountCredentialsDTO dto);
        bool RevokeToken(string username);
        User Update(User user);
    }
}
