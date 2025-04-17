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
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper, IUserRepository userRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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
            Guid questionId = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var answerModel = new Answer
            {
                Answer_id = Guid.NewGuid(),
                Question_id = questionId,  //add later

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
