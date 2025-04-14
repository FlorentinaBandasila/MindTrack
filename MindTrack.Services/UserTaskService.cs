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
        private readonly IMapper _mapper;

        public UserTaskService(IUserTaskRepository userTaskRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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
            var userTaskModel = new UserTask
            {
                Task_id = Guid.NewGuid(),
                User_id = user.User_id,
                Category_id = Guid.NewGuid(), //add later

                Title = userTaskDTO.Title,
                Priority = userTaskDTO.Title,
                Details = userTaskDTO.Title,
                Status = userTaskDTO.Title,

                End_date = DateTime.Now,
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
