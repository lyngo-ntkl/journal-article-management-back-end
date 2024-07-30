using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    public enum ReviewRequestStatus
    {
        ACCEPTED, REJECTED, NOT_RESPONSE
    }
    public class ReviewRequest: BaseEntity {
        public int ReviewerId { get; set; }
        [ForeignKey(nameof(ReviewerId))]
        public virtual User? Reviewer { get; set; }
        public int ArticleId { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public virtual Article? Article { get; set; }
        public ReviewRequestStatus Status { get; set; }
    }
}