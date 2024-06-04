namespace API.Dto.Responses
{
    public class AuthenticationResponse
    {
        public required string AccessToken { get; set; }
        // TODO: refresh token
    }
}
