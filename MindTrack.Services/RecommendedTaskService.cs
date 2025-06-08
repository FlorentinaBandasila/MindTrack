using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models;
using MindTrack.Models.Migrations;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Models.Data;

namespace MindTrack.Services
{
    public class RecommendedTaskService : IRecommendedTaskService
    {
        private readonly IRecommendedTaskRepository _recommendedTaskRepository;
        private readonly IMapper _mapper;
        private readonly IQuizResultsService _quizResultsService;
        private readonly MindTrackContext _context;

        public RecommendedTaskService(IRecommendedTaskRepository recommendedTaskRepository, IMapper mapper, IQuizResultsService quizResultsService, MindTrackContext context)
        {
            _recommendedTaskRepository = recommendedTaskRepository;
            _mapper = mapper;
            _quizResultsService = quizResultsService;
            _context = context;

        }

        public async Task<IEnumerable<RecommendedTask>> GetAllRecommendedTasks(string mood)
        {
            var recommendedTasks = await _recommendedTaskRepository.GetAllRecommendedTasks();

            var filteredTasks = recommendedTasks
                .Where(t => t.Mood != null && t.Mood.Equals(mood, StringComparison.OrdinalIgnoreCase))
                .Take(1)
                .ToList();

            foreach (var task in filteredTasks)
            {
                task.End_date = DateTime.Today;
            }

            return _mapper.Map<IEnumerable<RecommendedTask>>(filteredTasks);
        }
        public async Task AssignDailyRecommendedTasks(Guid userId)
        {
            var today = DateTime.Today;

            bool hasTodayTask = await _context.UserTasks
                .AnyAsync(ut => ut.User_id == userId
                                && ut.Recommended_Task_Id != null
                                && ut.Created_date.Date == today);

            if (hasTodayTask)
            {
                Console.WriteLine("User already has a recommended task for today.");
                return;
            }

            var quizResultDto = await _quizResultsService.GetQuizResultsByUser(userId);
            if (quizResultDto == null || string.IsNullOrEmpty(quizResultDto.Title))
                return;

            var mood = quizResultDto.Title;

            var alreadyUsedTaskIds = await _context.UserTasks
                .Where(ut => ut.User_id == userId && ut.Recommended_Task_Id != null)
                .Select(ut => ut.Recommended_Task_Id.Value)
                .ToListAsync() ?? new List<Guid>();

            var newRecommendedTasks = await _context.RecommendedTasks
                .Where(rt => rt.Mood == mood && !alreadyUsedTaskIds.Contains(rt.Recommended_Task_Id))
                .OrderBy(r => Guid.NewGuid()) 
                .Take(1)
                .ToListAsync();

            if (!newRecommendedTasks.Any())
                return;

            var defaultCategoryId = await _context.TaskCategories
                .Select(c => c.Category_id)
                .FirstOrDefaultAsync();

            var newUserTasks = newRecommendedTasks.Select(rt => new UserTask
            {
                Task_id = Guid.NewGuid(),
                User_id = userId,
                Category_id = defaultCategoryId,
                Title = rt.Title,
                Priority = rt.Priority,
                Details = rt.Details,
                Created_date = today,
                End_date = today,
                Status = "todo",
                Recommended_Task_Id = rt.Recommended_Task_Id
            });

            _context.UserTasks.AddRange(newUserTasks);
            await _context.SaveChangesAsync();
        }

    }
}
