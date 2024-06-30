using API.Dto.Responses;
using API.Repositories;
using API.Services;
using API.Utils;
using AutoMapper;
using Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTesting.User {
    public class AuthenticationTest {
        private UserService _userService;
        [SetUp]
        public void SetUp() {
            var unitOfWork = new Mock<UnitOfWork>();
            unitOfWork.Setup(uow => uow.UserRepository.GetAllAsync()).ReturnsAsync(new List<API.Entities.User> {
                new API.Entities.User {
                    Id = 1,
                    Email = "user@example.com",
                    Name = "User",
                    Role = API.Entities.Role.READER,
                    PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                    PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
                }
            });
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(m => m["security:secret-key"]).Returns(Key.SecuritySecretKey);
            var mapper = new Mock<IMapper>();
            var httpContextAccessor = new HttpContextAccessor();
            _userService = new UserServiceImplementation(unitOfWork.Object, configuration.Object, mapper.Object, httpContextAccessor);
        }

        [Test]
        public async Task TestAuthenticationSuccessfully() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "user@example.com",
                Password = "HelloWorld12345!!"
            };
            var actual = await _userService.LoginWithEmailPassword(request);

            Assert.NotNull(actual);
            Assert.IsNotEmpty(actual.AccessToken);
        }

        [Test]
        public void TestAuthenticationEmailNotFound() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "auser@example.com",
                Password = "HelloWorld12345!!"
            };

            var exception = Assert.ThrowsAsync<Exception>(async () => await _userService.LoginWithEmailPassword(request));
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessage.EmailNotFound));
        }

        [Test]
        public void TestAuthenticationWrongPassword() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "user@example.com",
                Password = "HelloWorld12345!"
            };

            var exception = Assert.ThrowsAsync<Exception>(async delegate { await _userService.LoginWithEmailPassword(request);});
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessage.PasswordNotFound));
        }
    }
}