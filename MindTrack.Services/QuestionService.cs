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

        public async Task<List<QuestionQuizDTO>> GetAllQuestionsWithAnswers()
        {

            var question = await _questionRepository.GetAllQuestionsWithAnswers();
            var result = question.Select(q => new QuestionQuizDTO
            {
                Question_id = q.Question_id,
                Title = q.Title,
                Answers = q.Answers.Select(a => new AnswerQuizDTO
                {
                    Answer_id = a.Answer_id,
                    Answer_name = a.Answer_name,
                    Points = a.Points
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<int> CalculateTotalPoints(List<UserAnswerDTO> userAnswers)
        {
            int totalPoints = 0;

            foreach (var userAnswer in userAnswers)
            {
                var question = await _questionRepository.GetQuestionById(userAnswer.Question_id);
                if (question != null)
                {
                    var answer = question.Answers.FirstOrDefault(a => a.Answer_id == userAnswer.Answer_id);
                    if (answer != null)
                    {
                        totalPoints += answer.Points;
                    }
                }
            }

            return totalPoints;
        }

    }
}
