namespace API.Entities {
    public class Topic: BaseEntity {
        public required string TopicName {get; set;}
        public virtual ICollection<Article> Articles {get; set;} = null!;
    }
}