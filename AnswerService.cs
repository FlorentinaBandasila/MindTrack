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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper, IUserRepository userRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            var answers = await _answerRepository.GetAllAnswers();
            return _mapper.Map<IEnumerable<Answer>>(answers);
        }

        public async Task<AnswerDTO> GetAnswerById(Guid id)
        {
            var answer = await _answerRepository.GetAnswerById(id);
            return _mapper.Map<AnswerDTO>(answer);
        }
        public async Task CreateAnswer(AnswerDTO answerDTO)
        {
            var user = await _userRepository.GetUserById(answerDTO.Answer_id);
            var questionId =await _questionRepository.GetQuestionById(answerDTO.Question_id);
            var answerModel = new Answer
            {
                Answer_id = Guid.NewGuid(),
                Question_id = questionId.Question_id,

                Answer_name = answerDTO.Answer_name,
                Points = answerDTO.Points,

                Created_time = DateTime.Now
            };

            await _answerRepository.CreateAnswer(answerModel);
        }

        public async Task DeleteAnswer(Guid id)
        {
            await _answerRepository.DeleteAnswer(id);
        }
    }
}
