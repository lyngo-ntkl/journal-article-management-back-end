using System.Net;
using System.Net.Http.Json;
using API.Configurations;
using API.Dto.Requests;
using API.Repositories;
using API.Services;
using API.Utils;
using AutoMapper;
using Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTesting.User {
    public class AccountRegistrationTest {
        private TestingServiceSetUp _setup;
        [SetUp]
        public void SetUp() {
            _setup = new TestingServiceSetUp();
            _setup.UnitOfWork.Setup(uow => uow.UserRepository.GetAll(default, default, "")).Returns(new List<API.Entities.User> {
                new API.Entities.User {
                    Id = 1,
                    Email = "user@example.com",
                    Name = "User",
                    Role = API.Entities.Role.READER,
                    PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                    PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
                }
            });
        }

        [Test]
        public void TestAccountRegistrationSuccessfully() {
            var request = new EmailPasswordRegistrationRequest {
                Email = "user1@gmail.com",
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = "User 1"
            };
            
            Assert.DoesNotThrowAsync(async delegate { await _setup.UserService.RegisterAccount(request); });
        }

        [Test]
        public void TestAccountRegistrationEmailExist() {
            var request = new EmailPasswordRegistrationRequest {
                Email = "user@example.com",
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = "User 1"
            };
            var exception = Assert.ThrowsAsync<Exception>(async delegate { await _setup.UserService.RegisterAccount(request); });
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessage.RegisteredEmail));
        }
    }
}