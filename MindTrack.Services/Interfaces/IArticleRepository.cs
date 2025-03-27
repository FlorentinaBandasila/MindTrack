using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article> GetArticleById(Guid id);
        Task CreateArticle(Article article);
        Task DeleteArticle(Guid id);
    }
}
