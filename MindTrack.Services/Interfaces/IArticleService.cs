using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<ArticleDTO> GetArticleById(Guid id);
        Task CreateArticle(Article article);
        Task DeleteArticle(Guid id);
    }
}
