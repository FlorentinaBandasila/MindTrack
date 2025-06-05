using Microsoft.AspNetCore.Mvc;
using MindTrack.Services.Interfaces;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendedTaskController: ControllerBase
    {
        private readonly IRecommendedTaskService _recommendedTaskService;

        public RecommendedTaskController(IRecommendedTaskService recommendedTaskService)
        {
            _recommendedTaskService = recommendedTaskService ?? throw new ArgumentNullException(nameof(recommendedTaskService));
        }


        [HttpGet("mood/{mood}")]
        public async Task<IActionResult> GetTasksByMood(string mood)
        {
            var tasks = await _recommendedTaskService.GetAllRecommendedTasks(mood);
            return Ok(tasks);
        }

    }
}
