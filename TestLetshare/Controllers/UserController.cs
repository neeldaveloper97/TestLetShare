using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestLetshare.Application.Features.User.Interface;

namespace TestLetshare.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var list = await _userService.GetAllUsers();
            return Ok(list);
        }
    }
}
