using Entities.Enums;

namespace Entities
{
    public class Review
    {
        public int Id { get; set; }
        public required Recommendation Recommendation { get; set; }
        public required string Summary { get; set; }
        public required string Strength { get; set; }
        public ICollection<Issue> Issues { get; set; } = null!;
        public required int ReviewerId { get; set; }
        public required int ManuscriptId { get; set; }
    }
}
