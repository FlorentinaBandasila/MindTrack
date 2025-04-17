using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAllAnswers();
        Task<Answer> GetAnswerById(Guid id);
        Task CreateAnswer(Answer answer);
        Task DeleteAnswer(Guid id);
    }
}
