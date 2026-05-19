using Microsoft.IdentityModel.JsonWebTokens;
using RWAN10T.Api.Auth.Config;
using RWAN10T.Api.Auth.Contract;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;
using System.Security.Claims;

namespace RWAN10T.Api.Services.Impl
{
    public class LoginServices : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly IUserAuthService _userAuthService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenService;
        private readonly TokenConfiguration _configurations;

        public LoginServices(IUserAuthService userAuthService, IPasswordHasher passwordHasher, ITokenGenerator tokenService, TokenConfiguration configurations)
        {
            _userAuthService = userAuthService;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configurations = configurations;
        }

        public AccountCredentialsDTO Create(AccountCredentialsDTO userDto)
        {
            var user = _userAuthService
                .Create(userDto);

            return new AccountCredentialsDTO
            {
                Username = user.Username,
                Fullname = user.Fullname,
                Password = "************"
            };
        }

        public bool RevokeToken(string username)
        {
            return _userAuthService
                .RevokeToken(username);
        }

        public TokenDTO? ValidateCredentials(UserDTO userDto)
        {
            var user = _userAuthService.FindByUserName(userDto.Username);

            if (user == null) return null;

            if(!_passwordHasher.Verify(userDto.Password, user.Password)) return null;

            return GenerateToken(user);

        }

        public TokenDTO? ValidateCredentials (TokenDTO token)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token.AccesToken);
            var username = principal?.Identity?.Name;
            var user = _userAuthService.FindByUserName(username!);

            if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            return GenerateToken(user, principal!.Claims);

        }

        private TokenDTO? GenerateToken(User user, IEnumerable<Claim>? existingClaims = null)
        {
            var claims = existingClaims?.ToList() ??
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            ];

            var accestToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configurations.DaysToExpiry);

            _userAuthService.Update(user);

            var createdDate = DateTime.Now;
            var expirationDate = createdDate.AddMinutes(_configurations.Minutes);

            return new TokenDTO
            {
                Authenticated = true,
                Created = createdDate.ToString(DATE_FORMAT),
                Expiration = expirationDate.ToString(DATE_FORMAT),
                AccesToken = accestToken,
                RefreshToken = refreshToken
            };
        }
    }
}
