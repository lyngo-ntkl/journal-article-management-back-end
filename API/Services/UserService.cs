using API.Dto.Requests;
using API.Dto.Responses;
using API.Repositories;
using API.Utils;

namespace API.Services {
    public interface UserService {
        Task<AuthenticationResponse> loginWithEmailPassword(EmailPasswordAuthenticationRequest request);
    }

    public class UserServiceImplementation : UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly JwtUtils _jwtUtils;

        public UserServiceImplementation(UnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = new JwtUtils(configuration);
        }

        public async Task<AuthenticationResponse> loginWithEmailPassword(EmailPasswordAuthenticationRequest request)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var user = users.FirstOrDefault(user => user.Email == request.Email);
            if (user == null)
            {
                throw new Exception(ExceptionMessage.EmailNotFound);
            }
            // TODO: password hashing
            if (user.Password != request.Password)
            {
                throw new Exception(ExceptionMessage.PasswordNotFound);
            }

            string accessToken = _jwtUtils.GenerateAccessToken(user);
            AuthenticationResponse response = new AuthenticationResponse
            {
                AccessToken = accessToken
            };

            return response;
        }
    }
}