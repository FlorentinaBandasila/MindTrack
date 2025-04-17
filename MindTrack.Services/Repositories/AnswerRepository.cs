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
    public class AnswerRepository: IAnswerRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public AnswerRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            return await _mindTrackContext.Answers.ToListAsync();
        }

        public async Task<Answer> GetAnswerById(Guid id)
        {
            return await _mindTrackContext.Answers.FirstOrDefaultAsync(u => u.Answer_id == id);
        }

        public async Task CreateAnswer(Answer answer)
        {
            await _mindTrackContext.Answers.AddAsync(answer);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task DeleteAnswer(Guid id)
        {
            var answer = await _mindTrackContext.Answers.FindAsync(id);

            if (answer != null) _mindTrackContext.Answers.Remove(answer);

            await _mindTrackContext.SaveChangesAsync();

        }
    }
}
