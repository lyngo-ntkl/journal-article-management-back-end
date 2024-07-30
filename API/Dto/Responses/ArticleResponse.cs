using API.Entities;

namespace API.Dto.Responses {
    public class ArticleResponse {
        public int Id {get; set;}
        public string? Title {get; set;}
        public string? Abstract {get; set;}
        public string? Introduction {get; set;}
        public string? Method {get; set;}
        public string? Results {get; set;}
        public ArticleStatus Status {get; set;}
        public string? Conclusion {get; set;}
        public string? DoiNumber {get; set;}
        public string? Discussion {get; set;}
        public string? FilePath {get; set;}
        public ICollection<UserResponse> Authors {get; set;} = null!;
        // public virtual ICollection<Topic> Topics {get; set;} = null!;
        // public virtual ICollection<Reference> References {get; set;} = null!;
        // public virtual ICollection<Reference> CitationBy {get; set;} = null!;
    }
}