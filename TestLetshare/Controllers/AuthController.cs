using Microsoft.AspNetCore.Mvc;
using TestLetshare.Application.Features.Auth.Commands;
using TestLetshare.Application.Features.Auth.Interfaces;

namespace TestLetshare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInCommand request)
        {
            try
            {
                var result = await _authService.SignInAsync(request);
                return result.Success ? Ok(result) : Unauthorized(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }
    }
}
