using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services;
using MindTrack.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models.Data;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTaskController: ControllerBase
    {
        private readonly IUserTaskService _userTaskService;
        private readonly MindTrackContext _mindTrackContext;
        public UserTaskController(IUserTaskService userTaskService, MindTrackContext mindTrackContext)
        {
            _userTaskService = userTaskService;
            _mindTrackContext = mindTrackContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserTasks()
        {
            var userTasks = await _userTaskService.GetAllUserTasks();
            if (userTasks == null) return NotFound();
            return Ok(userTasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTaskById(Guid id)
        {
            var userTask = await _userTaskService.GetUserTaskById(id);
            if (userTask == null)
            {
                return NotFound();
            }
            return Ok(userTask);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserTask([FromBody] UserTaskDTO userTaskDTO)
        {


            await _userTaskService.CreateUserTask(userTaskDTO);

            return Ok("User Task created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userTaskService.DeleteUserTask(id);
            return Ok("User Task deleted successfully");
        }

        [HttpPut("{id}/update-status/")]
        public async Task<IActionResult> UpdateAvatar([FromBody] UpdateTaskStatusDTO dto, [FromRoute] Guid id)
        {
            var user = await _mindTrackContext.UserTasks.FirstOrDefaultAsync(u => u.Task_id == id);

            if (user == null)
                return NotFound();

            user.Status = dto.Status;
            await _mindTrackContext.SaveChangesAsync();

            return Ok(new { avatar = user.Status });
        }
    }
}
