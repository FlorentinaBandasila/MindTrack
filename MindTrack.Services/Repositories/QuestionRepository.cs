using MindTrack.Models.Data;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MindTrack.Services.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public QuestionRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _mindTrackContext.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionById(Guid id)
        {
            return await _mindTrackContext.Questions.FirstOrDefaultAsync(a => a.Question_id == id);
        }

        public async Task CreateQuestion(Question question)
        {
            await _mindTrackContext.Questions.AddAsync(question);
            await _mindTrackContext.SaveChangesAsync();
        }

    }
}
