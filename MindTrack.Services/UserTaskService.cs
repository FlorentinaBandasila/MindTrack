using AutoMapper;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskCategoryRepository _taskCategory;
        private readonly IMapper _mapper;

        public UserTaskService(IUserTaskRepository userTaskRepository, IMapper mapper, IUserRepository userRepository, ITaskCategoryRepository taskCategory)
        {
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _taskCategory = taskCategory;
        }

        public async Task<IEnumerable<UserTask>> GetAllUserTasks()
        {
            var userTasks = await _userTaskRepository.GetAllUserTasks();
            return _mapper.Map<IEnumerable<UserTask>>(userTasks);
        }


        public async Task<UserTaskDTO> GetUserTaskById(Guid id)
        {
            var userTask = await _userTaskRepository.GetUserTaskById(id);
            return _mapper.Map<UserTaskDTO>(userTask);
        }
        public async Task CreateUserTask(UserTaskDTO userTaskDTO)
        {
            var user = await _userRepository.GetUserById(userTaskDTO.User_id);
            //var task = await _taskCategory.GetTaskCategoryById(userTaskDTO.Category_id);
            var task = new Guid("854E0CA3-0D26-4E6D-B1A2-096020887FD1");
            var userTaskModel = new UserTask
            {
                Task_id = Guid.NewGuid(),
                User_id = user.User_id,
                Category_id = task,

                Title = userTaskDTO.Title,
                Priority = userTaskDTO.Priority,
                Details = userTaskDTO.Details,
                Status = "todo",

                End_date = userTaskDTO.End_date,
                Created_date = DateTime.Now
            };

            await _userTaskRepository.CreateUserTask(userTaskModel);
        }

        public async Task DeleteUserTask(Guid id)
        {
            await _userTaskRepository.DeleteUserTask(id);
        }
    }
}
