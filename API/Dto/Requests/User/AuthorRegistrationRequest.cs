using System.ComponentModel.DataAnnotations;

namespace API.Dto.Requests {
    public class AuthorRegistrationRequest {
        [Required]
        public required AffiliationRequest Affiliation { get; set; }
    }
}