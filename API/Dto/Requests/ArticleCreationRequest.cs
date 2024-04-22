using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Dto.Requests {

    [JsonDerivedType(typeof(ArticleCreationRequestFile), typeDiscriminator: "file")]
    [JsonDerivedType(typeof(ArticleCreationRequestText), typeDiscriminator:"text")]
    public class ArticleCreationRequest {
        [Required]
        public ICollection<int>? AuthorIds;
        [Required]
        public ICollection<int>? TopicIds;
    }

    public class ArticleCreationRequestFile: ArticleCreationRequest {
        [Required]
        public string? FilePath {get; set;}
    }

    public class ArticleCreationRequestText: ArticleCreationRequest {
        [Required]
        public string? Abstract {get; set;}
        [Required]
        public string? Introduction {get; set;}
        [Required]
        public string? Method {get; set;}
        [Required]
        public string? Results {get; set;}
        // public virtual ICollection<Reference> References {get; set;} = null!;
    }
}