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
    public class TaskCategoryRepository : ITaskCategoryRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public TaskCategoryRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<TaskCategory>> GetAllTaskCategories()
        {
            return await _mindTrackContext.TaskCategories. ToListAsync();
        }

        public async Task<TaskCategory> GetTaskCategoryById(Guid id)
        {
            return await _mindTrackContext.TaskCategories.FirstOrDefaultAsync(a => a.Category_id == id);
        }

        public async Task CreateTaskCategory(TaskCategory taskCategory)
        {
            await _mindTrackContext.TaskCategories.AddAsync(taskCategory);
            await _mindTrackContext.SaveChangesAsync();
        }
    }
}
