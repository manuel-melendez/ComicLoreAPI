using ComicLoreApi.Models;
using ComicLoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicLoreApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var user = await _userService.Register(userDto);
            if(user == null)
            {
                return BadRequest("User registration failed. Please try again.");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _userService.Login(userLoginDto);
            if (!result.Success)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(result.Token);
        }

    }
}
