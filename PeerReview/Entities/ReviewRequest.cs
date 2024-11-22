using Entities.Enums;

namespace Entities
{
    public class ReviewRequest
    {
        private DateTime _timeoutDate;
        public int Id { get; set; }
        public required ReviewRequestStatus Status { get; set; } = ReviewRequestStatus.Sent;
        public required DateTime RequestTimeout {
            get
            {
                return _timeoutDate;
            }
            init
            {
                _timeoutDate = DateTime.UtcNow.AddDays(3);
            }
        }
        public required int ArticleId { get; set; }
        public required int ReviewerId { get; set; }
    }
}
