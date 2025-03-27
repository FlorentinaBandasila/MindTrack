using AutoMapper;
using MindTrack.Models;
using MindTrack.Models.DTOs;
using MindTrack.Services.Interfaces;
using MindTrack.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services
{
    public class ArticleService: IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            var articles = await _articleRepository.GetAllArticles();
            return _mapper.Map<IEnumerable<Article>>(articles);
        }

        public async Task<ArticleDTO> GetArticleById(Guid id)
        {
            var article = await _articleRepository.GetArticleById(id);
            return _mapper.Map<ArticleDTO>(article);
        }
        public async Task CreateArticle(Article article)
        {
            await _articleRepository.CreateArticle(article);
        }
    }
}
