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

        //[HttpPost]
        //public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        //{
        //    var user = new User
        //    {
        //        User_id = Guid.NewGuid(),
        //        Username = userDTO.Username,
        //        Email = userDTO.Email,
        //        Phone = userDTO.Phone,
        //        Password = userDTO.Password,
        //        Full_name = userDTO.Full_name,
        //        Created = DateTime.Now
        //    };

        //    var existingUser = await _userService.GetUserByUsername(user.Username);
        //    if (existingUser != null)
        //    {
        //        return BadRequest("The username is already taken.");
        //    }

        //    await _userService.CreateUser(user);

        //    return Ok("User created successfully");
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.DeleteUser(id);
            return Ok("User deleted successfully");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] JsonPatchDocument<UserDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            try
            {
                var updatedUser = await _userService.UpdateUser(id, patchDoc);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
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
    }
}
