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
        private TestingServiceSetUp _setup;
        [SetUp]
        public void SetUp() {
            _setup = new TestingServiceSetUp();
            _setup.UnitOfWork.Setup(uow => uow.UserRepository.GetAllAsync(default, default, "")).ReturnsAsync(new List<API.Entities.User> {
                new API.Entities.User {
                    Id = 1,
                    Email = "user@example.com",
                    Name = "User",
                    Role = API.Entities.Role.READER,
                    PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                    PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
                }
            });
            _setup.Configuration.Setup(m => m["security:secret-key"]).Returns(Key.SecuritySecretKey);
        }

        [Test]
        public async Task TestAuthenticationSuccessfully() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "user@example.com",
                Password = "HelloWorld12345!!"
            };
            var actual = await _setup.UserService.LoginWithEmailPassword(request);

            Assert.NotNull(actual);
            Assert.IsNotEmpty(actual.AccessToken);
        }

        [Test]
        public void TestAuthenticationEmailNotFound() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "auser@example.com",
                Password = "HelloWorld12345!!"
            };

            var exception = Assert.ThrowsAsync<Exception>(async () => await _setup.UserService.LoginWithEmailPassword(request));
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessage.EmailNotFound));
        }

        [Test]
        public void TestAuthenticationWrongPassword() {
            var request = new API.Dto.Requests.EmailPasswordAuthenticationRequest {
                Email = "user@example.com",
                Password = "HelloWorld12345!"
            };

            var exception = Assert.ThrowsAsync<Exception>(async delegate { await _setup.UserService.LoginWithEmailPassword(request);});
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessage.PasswordNotFound));
        }
    }
}