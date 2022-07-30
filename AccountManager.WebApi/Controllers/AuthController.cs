using AccountManager.Business.Abstract;
using AccountManager.Dto.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);
            if (loginResult.Success == false)
                return BadRequest(loginResult);

            var tokenResult = await _authService.CreateAccessTokenAsync(loginResult.Data);
            if (tokenResult.Success == false)
                return BadRequest(tokenResult);
            return Ok(tokenResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);
            if (registerResult.Success == false)
                return BadRequest(registerResult);
            return Ok(registerResult);
        }
    }
}
