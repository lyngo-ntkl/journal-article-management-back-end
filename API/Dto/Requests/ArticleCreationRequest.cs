using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Dto.Requests {

    [JsonDerivedType(typeof(ArticleCreationRequestFile), typeDiscriminator: "file")]
    [JsonDerivedType(typeof(ArticleCreationRequestText), typeDiscriminator: "text")]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    public class ArticleCreationRequest {
        [Required]
        public ICollection<int>? AuthorIds {get; set;}
        [Required]
        public ICollection<int>? TopicIds {get; set;}
    }

    public class ArticleCreationRequestFile: ArticleCreationRequest {
        [Required]
        public IFormFile? File {get; set;}
    }

    public class ArticleCreationRequestText: ArticleCreationRequest {
        //TODO: length validation
        [Required]
        public string? Abstract {get; set;}
        [Required]
        public string? Introduction {get; set;}
        [Required]
        public string? Method {get; set;}
        [Required]
        public string? Results {get; set;}
        public ICollection<int>? ReferenceIds {get; set;}
    }
}