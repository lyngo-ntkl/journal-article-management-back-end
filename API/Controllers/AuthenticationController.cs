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

        [HttpPost]
        public async Task<AuthenticationResponse> loginWithEmailPassword(EmailPasswordAuthenticationRequest request)
        {
            return await _userService.loginWithEmailPassword(request);
        }
    }
}
