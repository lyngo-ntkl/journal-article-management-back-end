using API.Dto.Requests;
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
        public async void CreateNewArticle(ArticleCreationRequest request) {
            await _articleService.CreateNewArticle(request);
        }
    }
}