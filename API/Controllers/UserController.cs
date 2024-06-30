using API.Dto.Requests;
using API.Dto.Responses;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Users)]
    [ApiController]
    public class UserController {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<UserResponse> RegisterAuthor([FromBody] AuthorRegistrationRequest request) {
            return await _userService.RegisterAuthor(request);
        }
    }
}