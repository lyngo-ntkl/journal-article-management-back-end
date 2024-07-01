using API.Entities;

namespace API.Dto.Responses {
    public class UserResponse {
        public string? Email {get; set;}
        public string? Name {get; set;}
        public Role Role {get; set;}
        public Affiliation? Affiliation { get; set;}
    }
}