namespace API.Entities {
    public enum Role {
        AUTHOR,
        READER,
        EDITOR
    }

    public class User: BaseEntity {
        public required string Email {get; set;}
        // TODO: may change when add hash for password
        public string? Password {get; set;}
        public required string Name {get; set;}
        public required Role Role {get; set;}

        public virtual ICollection<Article> Articles {get; set;} = null!;
    }
}