﻿using Microsoft.AspNetCore.Mvc;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services;
using MindTrack.Services.Interfaces;

namespace MindTrack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController: ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticles();
            if (articles == null) return NotFound();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var article = await _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] ArticleDTO articleDTO)
        {
            var article = new Article
            {
                Article_id = Guid.NewGuid(),
                Title = articleDTO.Title,
                Link = articleDTO.Link,
                Created_date = DateTime.Now,
                Photo=articleDTO.Photo
            };

            await _articleService.CreateArticle(article);

            return Ok("Article created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _articleService.DeleteArticle(id);
            return Ok("Article deleted successfully");
        }
    }
}
