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
            var categoryOrder = new List<string>
            {
                "Positive",
                "Anxiety",
                "Depression",
                
            };

            var questions = await _questionRepository.GetAllQuestionsWithAnswers();

            var result = questions
                .OrderBy(q => categoryOrder.IndexOf(q.Category)) 
                .Select(q => new QuestionQuizDTO
                {
                    Question_id = q.Question_id,
                    Title = q.Title,
                    Answers = q.Answers
                        .OrderBy(a => a.Points)
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

            int depressionScore = categoryScores.GetValueOrDefault("Depression", 0); 
            int anxietyScore = categoryScores.GetValueOrDefault("Anxiety", 0);      
            int wellbeingScore = categoryScores.GetValueOrDefault("Positive", 0);  

            int vpi = depressionScore + anxietyScore;
            int ri = wellbeingScore;

            var result = new QuizResults
            {
                QuizResult_id = Guid.NewGuid(),
                User_id = userId,
                Points = vpi + ri,
                Title = GetTitleByScore(vpi, ri),
                Date = DateTime.UtcNow
            };

            await _quizResultsRepository.AddQuizResults(result);
            return result;
        }

        private string GetTitleByScore(int vpi, int ri)
        {
            if (vpi <= 9 && ri >= 30)
                return "Mental health thriving, resilient and stable";

            if (vpi <= 9 && ri < 30)
                return "Stable but low positivity, monitor well-being";

            if (vpi <= 19 && ri >= 30)
                return "Mild symptoms but emotionally resilient";

            if (vpi <= 19 && ri < 30)
                return "Mild vulnerability with emotional fatigue";

            if (vpi <= 29 && ri >= 30)
                return "Moderate distress but showing resilience";

            if (vpi <= 29 && ri < 30)
                return "Moderate distress with low resilience";

            if (vpi <= 39 && ri >= 30)
                return "Severe symptoms, retains emotional strength";

            if (vpi <= 39 && ri < 30)
                return "Severe mental health strain, low resilience";

            if (vpi <= 48)
                return "Critical distress, immediate support needed";

            return "Uncategorized Profile";
        }

    }
}
