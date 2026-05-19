using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Services;

namespace RWAN10T.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginService _loginService;
        private IUserAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILoginService loginService, IUserAuthService authService, ILogger<AuthController> logger)
        {
            _loginService = loginService;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult Signin([FromBody] UserDTO user)
        {
            if (user == null || String.IsNullOrWhiteSpace(user.Username) || String.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Username and password must be provided.");


            var token = _loginService.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh([FromBody] TokenDTO tokenDto)
        {
            if (tokenDto == null) return BadRequest("Invalid!");

            var token = _loginService.ValidateCredentials(tokenDto);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("revoke")]
        [Authorize]
        public IActionResult Revoke()
        {
            var username = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            var result = _loginService.RevokeToken(username);
            if (!result) return BadRequest();

            return NoContent();
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody] AccountCredentialsDTO user)
        {
            if (user == null) return BadRequest("Invalid!");

            var result = _loginService.Create(user);

            return Ok(result);
        }
    }
}
