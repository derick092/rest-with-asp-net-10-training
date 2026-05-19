using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Services
{
    public interface ILoginService
    {
        TokenDTO? ValidateCredentials(UserDTO userDto);
        TokenDTO? ValidateCredentials(TokenDTO token);
        bool RevokeToken(string username);
        AccountCredentialsDTO Create(AccountCredentialsDTO userDto);
    }
}
