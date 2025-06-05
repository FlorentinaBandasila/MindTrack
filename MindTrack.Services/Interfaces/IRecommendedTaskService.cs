using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Models;

namespace MindTrack.Services.Interfaces
{
    public interface IRecommendedTaskService
    {
        Task<IEnumerable<RecommendedTask>> GetAllRecommendedTasks(string mood);
        Task AssignDailyRecommendedTasks(Guid userId);

    }
}
