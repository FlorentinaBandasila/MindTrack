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
            var taskCategory = new TaskCategory
            {
                Category_id = new Guid(),
                Category_name = taskCategoryDTO.Category_name,
            };

            await _taskCategoryService.CreateTaskCategory(taskCategory);

            return Ok("Task Category introduced successfully");
        }
    }
}
