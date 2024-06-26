using Microsoft.EntityFrameworkCore;

namespace API.Entities {
    public enum Role {
            AUTHOR,
            READER,
            EDITOR
    }

    [Index(nameof(Email), IsUnique = true)]
    public class User: BaseEntity {
        public required string Email {get; set;}
        public required string PasswordSalt {get; set;}
        public required string PasswordHash {get; set;}
        public required string Name {get; set;}
        public required Role Role {get; set;}

        public virtual ICollection<Article> Articles {get; set;} = null!;
    }
}