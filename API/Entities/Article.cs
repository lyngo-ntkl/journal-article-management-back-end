using API.Enums;

namespace API.Entities {
    public class Article: BaseEntity {
        public required string Abstract {get; set;}
        public required string Introduction {get; set;}
        public required string Method {get; set;}
        public required string Results {get; set;}
        public ArticleStatus Status {get; set;}
        public string? DoiNumber {get; set;}
        public string? Discussion {get; set;}
        public string? FilePath {get; set;}
        public virtual ICollection<User> Authors {get; set;} = null!;
        public virtual ICollection<Topic> Topics {get; set;} = null!;
        public virtual ICollection<Reference> References {get; set;} = null!;
        public virtual ICollection<Reference> CitationBy {get; set;} = null!;
    }

    public class Reference: BaseEntity {
        public int ArticleId {get; set;}
        public virtual Article? Article {get; set;}
        public int ReferenceArticleId {get; set;}
        public virtual Article? ReferenceArticle {get; set;}
    }
}