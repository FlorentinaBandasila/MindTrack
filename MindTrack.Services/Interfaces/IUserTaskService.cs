using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IUserTaskService
    {
        Task<IEnumerable<UserTask>> GetAllUserTasks();
        Task<UserTaskDTO> GetUserTaskById(Guid id);
        Task CreateUserTask(UserTaskDTO userTaskDTO);
        Task DeleteUserTask(Guid id);
        Task<IEnumerable<UserTaskDTO>> GetUserTasksForUser(Guid userId);
    }
}
