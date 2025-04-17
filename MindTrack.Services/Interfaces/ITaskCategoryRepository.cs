using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface ITaskCategoryRepository
    {
        Task<IEnumerable<TaskCategory>> GetAllTaskCategories();
        Task<TaskCategory> GetTaskCategoryById(Guid id);
        Task CreateTaskCategory(TaskCategory taskCategory);
    }
}
