using UseCases.ReviewRequests.Get;

namespace UseCases.ReviewRequests.Update
{
    public class RejectReviewRequestHandler : IRejectReviewRequest
    {
        public Task<ReviewRequestResponse> RejectReviewRequestAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
