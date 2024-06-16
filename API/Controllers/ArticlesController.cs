using API.Dto.Requests;
using API.Dto.Responses;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Articles)]
    [ApiController]
    public class ArticleController: ControllerBase {
        private ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("")]
        public async Task<ArticleResponse?> CreateNewArticle(ArticleCreationRequest request) {
            return await _articleService.CreateNewArticle(request);
        }

        [HttpGet("/{id}")]
        public async Task<ArticleResponse?> GetArticle(int id) {
            return await _articleService.GetArticle(id);
        }

        [HttpPut("/{id}")]
        public async Task<ArticleResponse?> UpdateArticle(int id, ArticleUpdateRequest request) {
            return await _articleService.UpdateArticle(id, request);
        }

        [HttpDelete("/drafts/{id}")]
        public async Task<ArticleResponse> DeleteDraftArticle(int id) {
            return await _articleService.DeleteDraftArticle(id);
        }

        [HttpDelete("/drafts/{id}/permanence")]
        public async Task<ArticleResponse> DeleteDraftArticlePermanent(int id) {
            return await _articleService.DeleteDraftArticlePermanent(id);
        }
    }
}