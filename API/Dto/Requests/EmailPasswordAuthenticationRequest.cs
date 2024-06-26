using System.ComponentModel.DataAnnotations;
using API.CustomAttributes;

namespace API.Dto.Requests
{
    public class EmailPasswordAuthenticationRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
        [Password]
        public required string Password { get; set; }
    }
}
