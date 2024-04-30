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
    }
}