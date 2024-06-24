using API.Enums;

namespace API.Entities {
    public class Article: BaseEntity {
        public string? Title {get; set;}
        public string? Abstract {get; set;}
        public string? Introduction {get; set;}
        public string? Method {get; set;}
        public string? Results {get; set;}
        public string? Discussion {get; set;}
        public string? Conclusion {get; set;}
        public ArticleStatus Status {get; set;}
        public string? DoiNumber {get; set;}
        public string? FilePath {get; set;}
        public virtual ICollection<User>? Authors {get; set;}
        public virtual ICollection<Topic>? Topics {get; set;}
        public virtual ICollection<Reference>? References {get; set;}
        public virtual ICollection<Reference>? CitationBy {get; set;}
    }

    public class Reference: BaseEntity {
        public int ArticleId {get; set;}
        public virtual Article? Article {get; set;}
        public int ReferenceArticleId {get; set;}
        public virtual Article? ReferenceArticle {get; set;}
    }
}