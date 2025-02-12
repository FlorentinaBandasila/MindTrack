using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            var user = new User
            {
                User_id = Guid.NewGuid(),
                Username = userDTO.Username,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Password = userDTO.Password,
                Full_name = userDTO.Full_name,
                Created = DateTime.Now
            };

            var existingUser = await _userService.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                return BadRequest("The username is already taken.");
            }

            await _userService.CreateUser(user);

            return Ok("User created successfully");
        }
    }
}
