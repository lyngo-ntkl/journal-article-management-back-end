namespace API.Dto.Requests {
    public class ArticleUpdateRequest {
        //TODO: length validation
        public string? Abstract {get; set;}
        public string? Introduction {get; set;}
        public string? Method {get; set;}
        public string? Results {get; set;}
        public string? Conclusion {get; set;}
        public IFormFile? File {get; set;}
        public ICollection<int>? AuthorIds {get; set;}
        public ICollection<int>? TopicIds {get; set;}
        public ICollection<int>? ReferenceIds {get; set;}
    }
}