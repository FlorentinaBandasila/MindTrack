using AutoMapper;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Repositories;

namespace MindTrack.Services
{
    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly IMapper _mapper;

        public TaskCategoryService(ITaskCategoryRepository taskCategoryRepository, IMapper mapper)
        {
            _taskCategoryRepository = taskCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskCategory>> GetAllTaskCategories()
        {
            var taskCategories = await _taskCategoryRepository.GetAllTaskCategories();
            return _mapper.Map<IEnumerable<TaskCategory>>(taskCategories);
        }

        public async Task<TaskCategoryDTO> GetTaskCategoryById(Guid id)
        {
            var taskCategory = await _taskCategoryRepository.GetTaskCategoryById(id);
            return _mapper.Map<TaskCategoryDTO>(taskCategory);
        }
        public async Task CreateTaskCategory(TaskCategory taskCategory)
        {
            await _taskCategoryRepository.CreateTaskCategory(taskCategory);
        }
    }
}
