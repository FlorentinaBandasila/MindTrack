using AutoMapper;
using MindTrack.Models;
using MindTrack.Models.DTOs;

namespace MindTrack.Web.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<UserTask, UserTaskDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Emotion, EmotionDTO>().ReverseMap();
            CreateMap<MoodSelection, MoodSelectionDTO>().ReverseMap();
            CreateMap<TaskCategory, TaskCategoryDTO>().ReverseMap();
        }
    }
}
