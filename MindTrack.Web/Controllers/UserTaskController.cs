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
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IRecommendedTaskService _recommendedTaskService;
        private readonly MindTrackContext _mindTrackContext;
        public UserTaskController(IUserTaskService userTaskService, MindTrackContext mindTrackContext, IRecommendedTaskService recommendedTaskService)
        {
            _userTaskService = userTaskService;
            _mindTrackContext = mindTrackContext;
            _recommendedTaskService = recommendedTaskService;
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
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTaskStatusDTO dto, [FromRoute] Guid id)
        {
            var user = await _mindTrackContext.UserTasks.FirstOrDefaultAsync(u => u.Task_id == id);

            if (user == null)
                return NotFound();

            user.Status = dto.Status;
            await _mindTrackContext.SaveChangesAsync();

            return Ok(new { status = user.Status });
        }

        [HttpGet("user/{userId}/today")]
        public async Task<IActionResult> GetTasksForToday(Guid userId)
        {
            
            await _recommendedTaskService.AssignDailyRecommendedTasks(userId);

            var tasks = await _userTaskService.GetUserTasksForUser(userId);

            return Ok(tasks);
        }

        [HttpGet("user/{userId}/weekly-progress")]
        public async Task<ActionResult<WeeklyProgressDTO>> GetWeeklyProgress(Guid userId)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var userTasks = await _mindTrackContext.UserTasks
                .Where(t => t.User_id == userId)
                .ToListAsync();

            var recommended = userTasks
                .Where(t => t.Recommended_Task_Id != null)
                .ToList();

            var userAdded = userTasks
                .Where(t =>
                    t.Recommended_Task_Id == null &&
                    t.Created_date >= startOfWeek &&
                    t.End_date <= endOfWeek)
                .ToList();

            var relevantTasks = recommended.Concat(userAdded).ToList();

            var completedTasks = relevantTasks
                .Count(t => t.Status == "done");

            var totalTasks = relevantTasks.Count;
            var percentage = totalTasks == 0 ? 100 : (completedTasks * 100) / totalTasks;

            var result = new WeeklyProgressDTO
            {
                Percentage = percentage,
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks
            };

            return Ok(result);
        }


    }
}
