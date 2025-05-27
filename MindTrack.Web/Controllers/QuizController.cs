using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Services;
using MindTrack.Services.Interfaces;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly  IQuestionService _questionService;
        private readonly IQuizResultsService _quizResultsService;

        public QuizController(IQuestionService questionService, IQuizResultsService quizResultsService)
        {
            _questionService = questionService;
            _quizResultsService = quizResultsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionsWithAnswers()
        {
            var questions = await _questionService.GetAllQuestionsWithAnswers();
            if (questions == null) return NotFound();
            return Ok(questions);
        }

        [HttpPost("user/{id}/submit-answers")]
        public async Task<IActionResult> SubmitAnswers([FromRoute] Guid id, [FromBody] List<UserAnswerDTO> userAnswers)
        {
            if (userAnswers == null || !userAnswers.Any())
                return BadRequest("No answers submitted.");

            var result = await _questionService.SaveQuiz(id, userAnswers);

            return Ok(new
            {
                result.QuizResult_id,
                result.Points,
                result.Title,
                result.Date
            });
        }

        [HttpGet("user/{id}/results")]
        public async Task<IActionResult> GetQuizResults([FromRoute] Guid id)
        {
            var results = await _quizResultsService.GetQuizResultsByUser(id);
            if (results == null) return NotFound();
            return Ok(results);
        }
    }
}
