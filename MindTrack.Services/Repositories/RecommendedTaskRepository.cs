using MindTrack.Models.Data;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrack.Services.Interfaces;

namespace MindTrack.Services.Repositories
{
    public class RecommendedTaskRepository : IRecommendedTaskRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public RecommendedTaskRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<RecommendedTask>> GetAllRecommendedTasks()
        {
            return await _mindTrackContext.RecommendedTasks.ToListAsync();
        }
    }
}
