using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IUserTaskRepository
    {
        Task<IEnumerable<UserTask>> GetAllUserTasks();
        Task<UserTask> GetUserTaskById(Guid id);
        Task CreateUserTask(UserTask userTask);
        Task DeleteUserTask(Guid id);
        //Task<UserTask> GetCategoryByName(string categoryName);
        //Task UpdateUserTask(UserTask userTask);
    }
}
