using API.Dto.Requests;
using API.Dto.Responses;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Authentication)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/email-password-auth")]
        public async Task<AuthenticationResponse> LoginWithEmailPassword(EmailPasswordAuthenticationRequest request)
        {
            return await _userService.LoginWithEmailPassword(request);
        }

        [HttpPost("/registration")]
        public async Task RegisterAccount(EmailPasswordRegistrationRequest request) {
            await _userService.RegisterAccount(request);
        }
    }
}
