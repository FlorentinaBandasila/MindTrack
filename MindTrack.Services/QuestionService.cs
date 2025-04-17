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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();
            return _mapper.Map<IEnumerable<Question>>(questions);
        }

        public async Task<QuestionDTO> GetQuestionById(Guid id)
        {
            var question = await _questionRepository.GetQuestionById(id);
            return _mapper.Map<QuestionDTO>(question);
        }
        public async Task CreateQuestion(Question question)
        {
            await _questionRepository.CreateQuestion(question);
        }

    }
}
