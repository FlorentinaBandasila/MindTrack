using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MindTrack.Models.Data;

namespace MindTrack.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly MindTrackContext _mindTrackContext;
        public UserController(IUserService userService, MindTrackContext mindTrackContext)
        {
            _userService = userService;
            _mindTrackContext = mindTrackContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.DeleteUser(id);
            return Ok("User deleted successfully");
        }

        

        [HttpPut("update-avatar/{id}")]
        public async Task<IActionResult> UpdateAvatar([FromBody] UpdateAvatarDTO dto, [FromRoute] Guid id)
        {
            var user = await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.User_id == id);

            if (user == null)
                return NotFound();

            user.Avatar = dto.Avatar;
            await _mindTrackContext.SaveChangesAsync();

            return Ok(new { avatar = user.Avatar });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await _userService.ForgotPasswordWithCode(email);
            if (!result)
                return NotFound("User with this email does not exist.");

            return Ok("Password reset code sent to email.");
        }

        [HttpPost("account-confirmation")]
        public async Task<IActionResult> AccountConfirmation([FromBody] string email)
        {
            var result = await _userService.ConfirmAccount(email);
            if (!result)
                return NotFound("User with this email does not exist.");

            return Ok("Account confirmation code sent to email.");
        }


        public class ResetPasswordCodeDTO
        {
            public string Email { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }

       
        [HttpPost("reset-password-code")]
        public async Task<IActionResult> ResetPasswordWithCode([FromBody] ResetPasswordCodeDTO model)
        {
            var result = await _userService.ResetPasswordWithCode(model.Email, model.Code, model.NewPassword);
            if (!result)
                return BadRequest("Invalid or expired reset code.");

            return Ok("Password has been reset successfully.");
        }

        [HttpPost("account-activation-code")]
        public async Task<IActionResult> AccountConfirmationWithCode([FromBody] ResetPasswordCodeDTO model)
        {
            var result = await _userService.AccountConfirmationWithCode(model.Email, model.Code);
            if (!result)
                return BadRequest("Invalid or expired reset code.");

            return Ok("Account is activated.");
        }
    }
}
