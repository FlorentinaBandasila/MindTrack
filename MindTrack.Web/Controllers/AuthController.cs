using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;

namespace MindTrack.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await userService.CreateUser(request);
            if (user is null) return BadRequest("User allready exists");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token = await userService.Login(request);
            if (token is null) return BadRequest("Invalid password or username");

            return Ok(token);
        }
    }
}
