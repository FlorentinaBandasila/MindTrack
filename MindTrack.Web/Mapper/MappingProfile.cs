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
        }
    }
}
