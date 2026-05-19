using RWAN10T.Api.Auth.Contract;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;
using RWAN10T.Api.Repositories;

namespace RWAN10T.Api.Services.Impl
{
    public class UserAuthServiceImpl(IUserRepository repository, IPasswordHasher hasher) : IUserAuthService
    {
        private readonly IUserRepository _repository = repository;
        private readonly IPasswordHasher _hasher = hasher;

        //Only for didatic purposes
        public User Create(AccountCredentialsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var user = new User
            {
                Username = dto.Username,
                Fullname = dto.Fullname,
                Password = _hasher.Hash(dto.Password),
                RefreshToken = String.Empty,
                RefreshTokenExpiryTime = null
            };

            return _repository.Create(user);
        }

        public User? FindByUserName(string username)
        {
            return _repository.FindByUserName(username);
        }

        public bool RevokeToken(string username)
        {
            var user = _repository.FindByUserName(username);
            if (user == null) return false;
            user.RefreshToken = null;
            _repository.Update(user);

            return true;
        }

        public User Update(User user)
        {
            return _repository.Update(user)!;
        }
    }
}
