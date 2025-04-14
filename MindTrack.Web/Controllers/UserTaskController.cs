using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services;
using MindTrack.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTaskController: ControllerBase
    {
        private readonly IUserTaskService _userTaskService;
        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
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
    }
}
