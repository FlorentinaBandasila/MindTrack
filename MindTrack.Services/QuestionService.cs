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
using MindTrack.Models.Migrations;

namespace MindTrack.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizResultsRepository _quizResultsRepository;
        private readonly IRecommendedTaskService _userTaskService;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IQuizResultsRepository quizResultsRepository, IRecommendedTaskService userTaskService)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _quizResultsRepository = quizResultsRepository;
            _userTaskService = userTaskService;
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
            // Define custom category order
            var categoryOrder = new List<string>
            {
                "1. Fatigue Severity",
                "2.1. Emotional State(Positive Affect)",
                "2.2. Emotional State(Negative Affect)",
                "3. Physical Activity & Lifestyle",
                "4. Anxiety Symptoms"
            };

            var questions = await _questionRepository.GetAllQuestionsWithAnswers();

            var result = questions
                .OrderBy(q => categoryOrder.IndexOf(q.Category)) // Order by category priority
                .Select(q => new QuestionQuizDTO
                {
                    Question_id = q.Question_id,
                    Title = q.Title,
                    Answers = q.Answers
                        .OrderBy(a => a.Points) // Order answers by points
                        .Select(a => new AnswerQuizDTO
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
            var categoryScores = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var userAnswer in userAnswers)
            {
                var question = await _questionRepository.GetQuestionById(userAnswer.Question_id);
                if (question == null) continue;

                var answer = question.Answers.FirstOrDefault(a => a.Answer_id == userAnswer.Answer_id);
                if (answer != null)
                {
                    if (!categoryScores.ContainsKey(question.Category))
                        categoryScores[question.Category] = 0;

                    categoryScores[question.Category] += answer.Points;
                }
            }

            var result = new QuizResults
            {
                QuizResult_id = Guid.NewGuid(),
                User_id = userId,
                Points = categoryScores.Values.Sum(),
                Title = GetTitleByScore(categoryScores),
                Date = DateTime.UtcNow
            };

            await _quizResultsRepository.AddQuizResults(result);
            return result;
        }

        private string GetTitleByScore(Dictionary<string, int> scores)
        {
            int fatigue = scores.GetValueOrDefault("Fatigue", 0);
            int pa = scores.GetValueOrDefault("Positive Affect", 0);
            int na = scores.GetValueOrDefault("Negative Affect", 0);
            int physical = scores.GetValueOrDefault("Physical Activity", 0);
            int anxiety = scores.GetValueOrDefault("Anxiety", 0);

         
            if (fatigue <= 25 && pa >= 40 && na <= 10 && physical <= 5 && anxiety <= 10)
                return "Optimal Wellness";

            if (fatigue == 70 && pa <= 10 && na >= 40 && physical == 17 && anxiety >= 51)
                return "Chronic Fatigue & High Anxiety";

            return "Uncategorized Profile";
        }

    }
}
