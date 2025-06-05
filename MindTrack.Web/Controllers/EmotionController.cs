using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Services;
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

        [HttpGet("user/{userId}/journal-by-user")]
        public async Task<IActionResult> GetMoodsByUser([FromRoute] Guid userId)
        {
            var emotions = await _emotionService.GetMoodByUser(userId);
            if (emotions == null) return NotFound();
            return Ok(emotions);
        }

        [HttpGet("user/{userId}/mood-chart")]
        public async Task<IActionResult> GetUserEmotionsGroupedByMood(Guid userId, int year, int month)
        {
            var result = await _emotionService.GetUserEmotionsGroupedByMood(userId, year, month);
            return Ok(result);
        }

        [HttpGet("user/{userId}/by-day")]
        public async Task<IActionResult> GetMoodByDay(Guid userId, int year, int month)
        {
            var result = await _emotionService.GetMoodByDay(userId, year, month);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmotion([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _emotionService.DeleteEmotion(id);
            return Ok("Emotion deleted successfully");
        }

    }
}
