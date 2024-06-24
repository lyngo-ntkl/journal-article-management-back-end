using System.ComponentModel.DataAnnotations;
using API.CustomAttributes;

namespace API.Dto.Requests {
    [Equal(nameof(Password), nameof(ConfirmedPassword))]
    public class EmailPasswordRegistrationRequest {
        [Required]
        [EmailAddress]
        public string? Email {get; set;}
        [Required]
        [Password]
        public string? Password {get; set;}
        [Required]
        public string? ConfirmedPassword {get; set;}
        [Required]
        public string? Name {get; set;}
    }
}