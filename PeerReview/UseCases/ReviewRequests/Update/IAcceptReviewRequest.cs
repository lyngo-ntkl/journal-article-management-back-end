using UseCases.ReviewRequests.Get;

namespace UseCases.ReviewRequests.Update
{
    public interface IAcceptReviewRequest
    {
        Task<ReviewRequestResponse> AcceptReviewRequestAsync(int id);
    }
}
