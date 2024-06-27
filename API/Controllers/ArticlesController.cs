using System.Collections.ObjectModel;
using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace API.Controllers {
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Articles)]
    [ApiController]
    // [AllowAnonymous]
    public class ArticleController: ControllerBase {
        private const string AuthorRole = "AUTHOR";
        private ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("text")]
        public async Task<ArticleResponse?> CreateNewArticleByText(ArticleCreationRequestText request) {
            return await _articleService.CreateNewArticleByText(request);
        }

        [HttpPost("file")]
        public async Task<ArticleResponse?> CreateNewArticleByFile([FromForm] ArticleCreationRequestFile request) {
            return await _articleService.CreateNewArticleByFile(request);
        }

        [Authorize(Roles = AuthorRole)]
        [HttpGet("personal")]
        public async Task<Collection<ArticleResponse>> GetPersonalArticles() {
            var authToken = HttpContext.Request.Headers.Authorization.ToString();
            return await _articleService.GetPersonalArticles(authToken);
        }

        [HttpGet("")]
        public async Task<Collection<ArticleResponse>> GetArticles() {
            return await _articleService.GetArticles();
        }

        [HttpGet("{id}")]
        public async Task<ArticleResponse?> GetArticle(int id) {
            return await _articleService.GetArticle(id);
        }

        [HttpPut("{id}")]
        public async Task<ArticleResponse?> UpdateArticle(int id, ArticleUpdateRequest request) {
            return await _articleService.UpdateArticle(id, request);
        }

        [HttpDelete("drafts/{id}")]
        public async Task<ArticleResponse> DeleteDraftArticle(int id) {
            return await _articleService.DeleteDraftArticle(id);
        }

        [HttpDelete("drafts/{id}/permanence")]
        public async Task<ArticleResponse> DeleteDraftArticlePermanent(int id) {
            return await _articleService.DeleteDraftArticlePermanent(id);
        }

        [HttpPut("submission/{id}")]
        public async Task<ArticleResponse> SubmitArticle(int id) {
            // TODO: Plagiarism checker
            return await _articleService.SubmitArticle(id);
        }
    }
}