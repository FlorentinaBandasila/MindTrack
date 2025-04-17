using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Services.Interfaces;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var answers = await _answerService.GetAllAnswers();
            if (answers == null) return NotFound();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(Guid id)
        {
            var answer = await _answerService.GetAnswerById(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer([FromBody] AnswerDTO answerDTO)
        {


            await _answerService.CreateAnswer(answerDTO);

            return Ok("Answer returned successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _answerService.DeleteAnswer(id);
            return Ok("Answer deleted successfully");
        }
    }
}
