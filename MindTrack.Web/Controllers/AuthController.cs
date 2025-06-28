using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;
using Google.Apis.Auth;
using MindTrack.Services;
using Newtonsoft.Json;

namespace MindTrack.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IUserService userService, IUserRepository userRepository, IConfiguration configuration, IUserRepository userRepo) : ControllerBase
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
        // DTO
        public class GoogleTokenDTO
        {
            public string AccessToken { get; set; } = null!;
        }

        // Controller
        [HttpPost("login/google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleTokenDTO dto)
        {
            // 1) call Google to get user info
            using var http = new HttpClient();
            var resp = await http.GetAsync(
              $"https://www.googleapis.com/oauth2/v3/userinfo?access_token={dto.AccessToken}");
            if (!resp.IsSuccessStatusCode)
                return BadRequest("Invalid Google access token");

            var json = await resp.Content.ReadAsStringAsync();
            dynamic info = JsonConvert.DeserializeObject(json)!;
            string email = info.email;

            // 2) lookup or create by email
            var user = await userRepo.GetUserByEmail(email);
            if (user == null)
            {
                var registerDto = new UserRegisterDTO
                {
                    Username = email.Split('@')[0],
                    Email = email,
                    Full_name = info.name,
                    Phone = "",
                    Password = Guid.NewGuid().ToString()
                };
                var result = await userService.CreateUser(registerDto);
                if (!result.Success)
                    return BadRequest(result.ErrorMessage);
                user = result.User!;
            }

            // 3) issue your JWT
            var token = userService.LoginWithGoogle(user);
            return Ok(new { token });
        }


    }
}
