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
    }

    public class UserServiceImplementation : UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly JwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserServiceImplementation(UnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = new JwtUtils(configuration);
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginWithEmailPassword(EmailPasswordAuthenticationRequest request)
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

        public async Task RegisterAccount(EmailPasswordRegistrationRequest request)
        {
            if (_unitOfWork.UserRepository.GetAll().Exists(user => user.Email == request.Email)) {
                throw new Exception(ExceptionMessage.RegisteredEmail);
            }
            var user = _mapper.Map<User>(request);
            user = await _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveAsync();
        }
    }
}