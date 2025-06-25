using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;

namespace MindTrack.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IUserService userService, IUserRepository userRepository) : ControllerBase
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

        [HttpPost("register/google")]
        public async Task<ActionResult<object>> RegisterGoogle(UserRegisterDTO request)
        {
            var existingUser = await userRepository.GetUserByEmail(request.Email);

            User user;

            if (existingUser != null)
            {
                user = existingUser;
            }
            else
            {
                var createResult = await userService.CreateUser(request);
                if (!createResult.Success)
                    return BadRequest(createResult.ErrorMessage);

                user = createResult.User;
            }

            var token = userService.LoginWithGoogle(user);
            return Ok(new { token, user });
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            var token = await userService.Login(request);
            if (token is null) return BadRequest("Invalid password or email/username");

            return Ok(token);
        }
    }
}
