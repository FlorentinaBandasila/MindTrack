using Microsoft.EntityFrameworkCore;
using MindTrack.Models;
using MindTrack.Models.Data;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly MindTrackContext _mindTrackContext; 
        public ArticleRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _mindTrackContext.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid id)
        {
            return await _mindTrackContext.Articles.FirstOrDefaultAsync(a =>a.Article_id==id);
        }

        public async Task CreateArticle(Article article)
        {
            await _mindTrackContext.Articles.AddAsync(article);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task DeleteArticle(Guid id)
        {
            var article = await _mindTrackContext.Articles.FindAsync(id);

            if (article != null) _mindTrackContext.Articles.Remove(article);

            await _mindTrackContext.SaveChangesAsync();

        }
    }
}
