using Entities.Enums;

namespace UseCases.Reviews.Update
{
    public class ReviewUpdateRequest
    {
        public required int Id { get; set; }
        public Recommendation? Recommendation { get; set; }
        public string? Summary { get; set; }
        public string? Strength { get; set; }
        public ICollection<IssueRequest> Issues { get; set; } = null!;
    }
}
