using UseCases.ReviewRequests.Get;

namespace UseCases.ReviewRequests.Update
{
    public interface IRejectReviewRequest
    {
        Task<ReviewRequestResponse> RejectReviewRequestAsync(int id);
    }
}
