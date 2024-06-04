using System.ComponentModel.DataAnnotations;

namespace API.Dto.Requests
{
    public class EmailPasswordAuthenticationRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
        // TODO: validate password format
        public required string Password { get; set; }
    }
}
