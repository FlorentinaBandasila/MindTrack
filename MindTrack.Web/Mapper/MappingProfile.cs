﻿using AutoMapper;
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
            CreateMap<Answer, AnswerQuizDTO>().ReverseMap();
            CreateMap<Question, QuestionQuizDTO>().ReverseMap();
            CreateMap<MoodSelection, MoodDTO>().ReverseMap();
            CreateMap<QuizResults, QuizResultsDTO>().ReverseMap();
            CreateMap<Emotion, JournalDTO>().ReverseMap();
        }
    }
}
