using MindTrack.Models.Data;
using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models.Migrations;

namespace MindTrack.Services.Repositories
{
    public class QuizResultsRepository: IQuizResultsRepository
    {
        

        private readonly MindTrackContext _mindTrackContext;

        public QuizResultsRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<QuizResults?> GetQuizResultsByUser(Guid id)
        {
            var result = await _mindTrackContext.QuizResults.FirstOrDefaultAsync(q => q.User_id == id);
            return result;
        }

        public async Task AddQuizResults(QuizResults result)
        {
            await _mindTrackContext.QuizResults.AddAsync(result);
            await _mindTrackContext.SaveChangesAsync();
        }
    }
}
