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
        public async Task<ActionResult> Register(UserRegisterDTO request)
        {
            var result = await userService.CreateUser(request);

            if (!result.Success)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(result.User);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            var token = await userService.Login(request);
            if (token is null) return BadRequest("Invalid password or email");

            return Ok(token);
        }
    }
}
