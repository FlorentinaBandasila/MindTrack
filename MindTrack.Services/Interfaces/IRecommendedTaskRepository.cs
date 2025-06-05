using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IRecommendedTaskRepository
    {
        Task<IEnumerable<RecommendedTask>> GetAllRecommendedTasks();
    }
}
