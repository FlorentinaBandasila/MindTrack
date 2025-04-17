using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<Answer>> GetAllAnswers();
        Task<AnswerDTO> GetAnswerById(Guid id);
        Task CreateAnswer(AnswerDTO answerDTO);
        Task DeleteAnswer(Guid id);
    }
}
