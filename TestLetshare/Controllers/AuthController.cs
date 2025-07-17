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

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] SignInCommand request)
        {
            try
            {
                var result = await _authService.SignInAsync(request);
                return result.Success ? Ok(result.Data) : Unauthorized(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }
    }
}
