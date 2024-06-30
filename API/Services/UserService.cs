using System.Security.Claims;
using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Repositories;
using API.Utils;
using AutoMapper;

namespace API.Services {
    public interface UserService {
        Task<AuthenticationResponse> LoginWithEmailPassword(EmailPasswordAuthenticationRequest request);
        Task RegisterAccount(EmailPasswordRegistrationRequest request);
        Task<UserResponse> RegisterAuthor(AuthorRegistrationRequest request);
    }

    public class UserServiceImplementation : UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly JwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServiceImplementation(UnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = new JwtUtils(configuration);
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationResponse> LoginWithEmailPassword(EmailPasswordAuthenticationRequest request)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var user = users.FirstOrDefault(user => user.Email == request.Email);
            if (user == null)
            {
                throw new Exception(ExceptionMessage.EmailNotFound);
            }
            
            HashingUtils.Hash(request.Password, user.PasswordSalt, out string passwordHash);
            if (passwordHash != user.PasswordHash)
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

        public async Task RegisterAccount(EmailPasswordRegistrationRequest request)
        {
            if (_unitOfWork.UserRepository.GetAll().Exists(user => user.Email == request.Email)) {
                throw new Exception(ExceptionMessage.RegisteredEmail);
            }

            var user = _mapper.Map<User>(request);
            HashingUtils.Hash(request.Password, out string salt, out string hash);
            user.PasswordSalt = salt;
            user.PasswordHash = hash;

            user = await _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserResponse> RegisterAuthor(AuthorRegistrationRequest request)
        {
            var id = int.Parse(_jwtUtils.GetSidClaim(_httpContextAccessor)!);

            var user = await _unitOfWork.UserRepository.GetAsync(id);
            if (user == null) {
                throw new Exception(ExceptionMessage.UserNotFound);
            }

            user.Affiliations = [
                _mapper.Map<Affiliation>(request.Affiliation)
            ];
            user.Role = Role.AUTHOR;

            user = _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserResponse>(user);
        }
    }
}