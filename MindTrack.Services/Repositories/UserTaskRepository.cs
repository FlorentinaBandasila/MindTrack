using MindTrack.Models.Data;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MindTrack.Services.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public UserTaskRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<UserTask>> GetAllUserTasks()
        {
            return await _mindTrackContext.UserTasks.ToListAsync();
        }

        public async Task<UserTask> GetUserTaskById(Guid id)
        {
            return await _mindTrackContext.UserTasks.FirstOrDefaultAsync(u => u.Task_id == id);
        }

        public async Task CreateUserTask(UserTask userTask)
        {
            await _mindTrackContext.UserTasks.AddAsync(userTask);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task DeleteUserTask(Guid id)
        {
            var userTask = await _mindTrackContext.UserTasks.FindAsync(id);

            if (userTask != null) _mindTrackContext.UserTasks.Remove(userTask);

            await _mindTrackContext.SaveChangesAsync();

        }

        //public async Task<UserTask> GetCategoryByName(string taskName)
        //{
        //    return await _mindTrackContext.UserTasks.FirstOrDefaultAsync(t => t.Task_categories == taskName);
        //}
    }
}
