using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface ITaskCategoryService
    {
        Task<IEnumerable<TaskCategory>> GetAllTaskCategories();
        Task<TaskCategoryDTO> GetTaskCategoryById(Guid id);
        Task CreateTaskCategory(TaskCategory taskCategory);
    }
}
