using Entities.Enums;

namespace UseCases.ReviewRequests.Get
{
    public class ReviewRequestResponse
    {
        public required int Id { get; set; }
        public required ReviewRequestStatus Status { get; set; }
        public required DateTime RequestTimeout { get; set; }
        public required int ArticleId { get; set; }
    }
}
