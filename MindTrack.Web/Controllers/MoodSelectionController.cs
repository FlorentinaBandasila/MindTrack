using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using MindTrack.Services;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodSelectionController : ControllerBase
    {
        private readonly IMoodSelectionService _moodSelectionService;
        public MoodSelectionController(IMoodSelectionService moodSelectionService)
        {
            _moodSelectionService = moodSelectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoodSelections()
        {
            var moodSelections = await _moodSelectionService.GetAllMoodSelections();
            if (moodSelections == null) return NotFound();
            return Ok(moodSelections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoodSelectionById(Guid id)
        {
            var moodSelection = await _moodSelectionService.GetMoodSelectionById(id);
            if (moodSelection == null)
            {
                return NotFound();
            }
            return Ok(moodSelection);
        }

        [HttpPost]
        public async Task<IActionResult> AddMoodSelection([FromBody] MoodSelectionDTO moodSelectionDTO)
        {
            var moodSelection = new MoodSelection
            {
                Mood_id = Guid.NewGuid(),
                Mood = moodSelectionDTO.Mood,
            };

            await _moodSelectionService.CreateMoodSelection(moodSelection);

            return Ok("Mood selected successfully");
        }
    }
}
