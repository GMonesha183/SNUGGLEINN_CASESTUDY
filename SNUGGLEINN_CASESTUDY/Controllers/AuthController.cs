using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.DTOs;
using SNUGGLEINN_CASESTUDY.Helpers;
using SNUGGLEINN_CASESTUDY.Models;
using SNUGGLEINN_CASESTUDY.Services;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = JwtHelper.GenerateToken(user, _config);
            return Ok(new
            {
                token,
                user = new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Role,
                    user.PhoneNumber
                }
            });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Password = registerDto.Password, // Ideally hash passwords
                Role = registerDto.Role,
                PhoneNumber = registerDto.PhoneNumber
            };

            await _userService.AddUserAsync(newUser);

            var token = JwtHelper.GenerateToken(newUser, _config);

            return Ok(new
            {
                token,
                user = new
                {
                    newUser.UserId,
                    newUser.FullName,
                    newUser.Email,
                    newUser.Role,
                    newUser.PhoneNumber
                }
            });
        }
    }
}
