using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.ReviewRequests.Get;
using UseCases.ReviewRequests.Update;

namespace Infrastructure.API.Controllers
{
    [Route("api/v1/review-requests")]
    [ApiController]
    public class ReviewRequestController(
        IAcceptReviewRequest acceptReviewRequest,
        IRejectReviewRequest rejectReviewRequest
        ) : ControllerBase
    {
        private readonly IAcceptReviewRequest _acceptReviewRequest = acceptReviewRequest;
        private readonly IRejectReviewRequest _rejectReviewRequest = rejectReviewRequest;

        [HttpPost("{id}/acceptance")]
        public async Task<ActionResult<ReviewRequestResponse>> AcceptAsync([FromRoute] int id)
        {
            return await _acceptReviewRequest.AcceptReviewRequestAsync(id);
        }

        [HttpPost("{id}/rejection")]
        public async Task<ActionResult<ReviewRequestResponse>> RejectAsync([FromRoute] int id)
        {
            return await _rejectReviewRequest.RejectReviewRequestAsync(id);
        }
    }
}
