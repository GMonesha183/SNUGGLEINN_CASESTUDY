using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.DTOs;
using SNUGGLEINN_CASESTUDY.Models;
using SNUGGLEINN_CASESTUDY.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Public registration for Guests
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto newUser)
        {
            var user = new User
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = "Guest",
                PhoneNumber = newUser.PhoneNumber
            };

            await _userService.AddUserAsync(user);
            return Ok("User registered successfully");
        }

        // Login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var token = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { token });
        }

        // Admin/Owner: Get all users
        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            return Ok(userDtos);
        }

        // Get user by ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");

            var userDto = new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userDto);
        }

        // Admin/Owner adds user
        [HttpPost("add")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterDto newUser)
        {
            var user = new User
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = newUser.Role,
                PhoneNumber = newUser.PhoneNumber
            };

            await _userService.AddUserAsync(user);
            return Ok("User added successfully");
        }

        // Admin/Owner updates user
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRegisterDto updatedUser)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;
            user.PhoneNumber = updatedUser.PhoneNumber;

            await _userService.UpdateUserAsync(user);
            return Ok("User updated successfully");
        }

        // Admin/Owner deletes user
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted successfully");
        }
    }
}
