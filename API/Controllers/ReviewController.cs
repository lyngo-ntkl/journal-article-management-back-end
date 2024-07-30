using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Review)]
    [ApiController]
    public class ReviewController {
        private readonly ReviewRequestService _reviewRequestService;

        public ReviewController(ReviewRequestService reviewRequestService)
        {
            _reviewRequestService = reviewRequestService;
        }

        [HttpPost("assignment/{articleId}")]
        public async Task AssignReviewers(int articleId) {
            await _reviewRequestService.AssignReviewers(articleId);
        }
    }
}