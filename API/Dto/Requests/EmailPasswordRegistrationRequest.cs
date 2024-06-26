using System.ComponentModel.DataAnnotations;
using API.CustomAttributes;

namespace API.Dto.Requests {
    [Equal(nameof(Password), nameof(ConfirmedPassword))]
    public class EmailPasswordRegistrationRequest {
        [Required]
        [EmailAddress]
        public required string Email {get; set;}
        [Required]
        [Password]
        public required string Password {get; set;}
        [Required]
        public required string ConfirmedPassword {get; set;}
        [Required]
        public required string Name {get; set;}
    }
}