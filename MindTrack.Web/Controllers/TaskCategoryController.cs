using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskCategoryController : ControllerBase
    {
        private readonly ITaskCategoryService _taskCategoryService;
        public TaskCategoryController(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskCategories()
        {
            var taskCategories = await _taskCategoryService.GetAllTaskCategories();
            if (taskCategories == null) return NotFound();
            return Ok(taskCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskCategoryById(Guid id)
        {
            var taskCategory = await _taskCategoryService.GetTaskCategoryById(id);
            if (taskCategory == null)
            {
                return NotFound();
            }
            return Ok(taskCategory);
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskCategory([FromBody] TaskCategoryDTO taskCategoryDTO)
        {
            Guid taskCategoryId = Guid.Parse("854E0CA3-0D26-4E6D-B1A2-096020887FD1");
            var taskCategory = new TaskCategory
            {
                Category_id = taskCategoryId,
                Category_name = taskCategoryDTO.Category_name,
            };

            await _taskCategoryService.CreateTaskCategory(taskCategory);

            return Ok("Task Category introduced successfully");
        }
    }
}
