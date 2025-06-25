using AutoMapper;
using MindTrack.Models;
using MindTrack.Models.DTOs;
using MindTrack.Models.Migrations;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services
{
    public class QuizResultsService : IQuizResultsService
    {
        private IQuizResultsRepository _quizResultsRepository;
        private IMapper _mapper;
        public QuizResultsService(IQuizResultsRepository quizResultsRepository)
        {
            _quizResultsRepository = quizResultsRepository;
        }

        public async Task<QuizResultsDTO> GetQuizResultsByUser(Guid id)
        {
            var quizresults = await _quizResultsRepository.GetQuizResultsByUser(id);
            return new QuizResultsDTO
            {
                Title = quizresults.Title,
                Date = quizresults.Date,
            };

        }
    }
}
