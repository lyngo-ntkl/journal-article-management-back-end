using Microsoft.EntityFrameworkCore;

namespace API.Entities {
    public enum Role {
            AUTHOR,
            READER,
            EDITOR
    }

    public class User: BaseEntity {
        // TODO: Unique annotation
        public required string Email {get; set;}
        public string? Password {get; set;}
        public required string Name {get; set;}
        public required Role Role {get; set;}

        public virtual ICollection<Article> Articles {get; set;} = null!;
    }
}