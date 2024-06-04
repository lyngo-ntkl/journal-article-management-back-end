using API.Entities.Enums;

namespace API.Entities {
    public class User: BaseEntity {
        public required string Email {get; set;}
        public string? Password {get; set;}
        public required string Name {get; set;}
        public required Role Role {get; set;}
    }
}