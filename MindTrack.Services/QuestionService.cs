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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizResultsRepository _quizResultsRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IQuizResultsRepository quizResultsRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _quizResultsRepository = quizResultsRepository;
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

        public async Task<QuizResults> SaveQuiz(Guid userId, List<UserAnswerDTO> userAnswers)
        {
            var totalPoints = 0;
            foreach (var userAnswer in userAnswers)
            {
                var question = await _questionRepository.GetQuestionById(userAnswer.Question_id);
                if (question == null) continue;

                var answer = question.Answers
                    .FirstOrDefault(a => a.Answer_id == userAnswer.Answer_id);
                if (answer != null)
                    totalPoints += answer.Points;
            }

            var title = GetTitleByScore(totalPoints);

            var result = new QuizResults
            {
                QuizResult_id = Guid.NewGuid(),
                User_id = userId,
                Points = totalPoints,
                Title = title,
                Date = DateTime.UtcNow
            };

            await _quizResultsRepository.AddQuizResults(result);

            return result;
        }

        string GetTitleByScore(int score)
        {
            switch (score)
            {
                case < 0:
                    return "Unknown";
                case < 40:
                    return "Needs Improvement";
                case <= 70:
                    return "Happy";
                default:
                    return "Ecstatic";
            }
        }
    }
}
