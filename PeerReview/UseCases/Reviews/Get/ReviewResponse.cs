using Entities.Enums;
using Entities;

namespace UseCases.Reviews.Get
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public Recommendation? Recommendation { get; set; }
        public string? Summary { get; set; }
        public string? Strength { get; set; }
        public ICollection<Issue> Issues { get; set; } = null!;
        public required int ReviewerId { get; set; }
        public required int ArticleId { get; set; }
    }
}
