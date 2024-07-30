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
        // TODO: may effect registration as author, submit, published, reference article
        public int? HIndex { get; set;}

        public virtual ICollection<Article> Articles {get; set;} = null!;
        public virtual ICollection<Affiliation> Affiliations { get; set;} = null!;
    }
}