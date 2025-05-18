using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(Guid id);
        Task CreateQuestion(Question question);
        Task<List<Question>> GetAllQuestionsWithAnswers();
    }
}
