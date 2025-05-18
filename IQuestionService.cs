using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<QuestionDTO> GetQuestionById(Guid id);
        Task CreateQuestion(Question question);

        Task<List<QuestionQuizDTO>> GetAllQuestionsWithAnswers();

        Task<int> CalculateTotalPoints(List<UserAnswerDTO> userAnswers);
    }
}
