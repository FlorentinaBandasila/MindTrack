using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmotionController : ControllerBase
    {
        private readonly IEmotionService _emotionService;
        public EmotionController(IEmotionService emotionService)
        {
            _emotionService = emotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmotions()
        {
            var emotions = await _emotionService.GetAllEmotions();
            if (emotions == null) return NotFound();
            return Ok(emotions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmotionById(Guid id)
        {
            var emotion = await _emotionService.GetEmotionById(id);
            if (emotion == null)
            {
                return NotFound();
            }
            return Ok(emotion);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmotion([FromBody] AddEmotionDTO emotionDTO)
        {

            await _emotionService.CreateEmotion(emotionDTO);

            return Ok("Emotion saved successfully");
        }

        [HttpGet("user/{id}/mood-chart")]
        public async Task<IActionResult> GetUserMoodReport(Guid id)
        {
            var result = await _emotionService.GetUserEmotionsGroupedByMood(id);
            return Ok(result);
        }

    }
}
