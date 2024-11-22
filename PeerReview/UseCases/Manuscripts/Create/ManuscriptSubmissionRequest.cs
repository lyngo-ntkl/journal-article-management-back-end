namespace UseCases.Manuscripts.Create
{
    public class ManuscriptSubmissionRequest
    {
        public required string Title { get; set; }
        public required int[] AuthorIds { get; set; }
        public required int TopicId { get; set; }
        public required int[] KeywordIds { get; set; }
        public required Stream FileStream { get; set; }
    }
}
