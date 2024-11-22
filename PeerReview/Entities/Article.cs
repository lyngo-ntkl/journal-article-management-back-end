using Entities.Enums;

namespace Entities
{
    public class Article
    {
        public int Id { get; set; }
        public required ArticleStatus Status { get; set; }
        public required string Title { get; set; }
        public required string Path { get; set; }
        public ICollection<User> Authors { get; set; } = null!;
        public ICollection<Keyword> Keywords { get; set; } = null!;
        public required int TopicId { get; set; }
    }
}
