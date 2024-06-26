using System.Net;
using System.Net.Http.Json;
using API.Dto.Requests;
using API.Utils;
using Constant;

namespace UnitTesting.User {
    public class AccountRegistrationTest {
        public const string TestEmail = "user1@gmail.com";
        public const string TestName = "User 1";
        [Test]
        public async Task TestAccountRegistrationInvalidPassword() {
            HttpClient client = new HttpClient();
            var request = new EmailPasswordRegistrationRequest {
                Email = TestEmail,
                Password = "Hello",
                ConfirmedPassword = "Hello",
                Name = TestName
            };
            var response = await client.PostAsync(Route.Host + Route.BaseUrl + ApiPath.Authentication + "/registration", JsonContent.Create(request));
            bool actual = response.IsSuccessStatusCode, expected = true;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        }

        [Test]
        public async Task TestAccountRegistrationInvalidEmail() {
            HttpClient client = new HttpClient();
            var request = new EmailPasswordRegistrationRequest {
                Email = "User",
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = TestName
            };
            var response = await client.PostAsync(Route.Host + Route.BaseUrl + Route.Authentication, JsonContent.Create(request));
            bool actual = response.IsSuccessStatusCode, expected = true;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task TestAccountRegistrationConfirmedPasswordNotMatch() {
            HttpClient client = new HttpClient();
            var request = new EmailPasswordRegistrationRequest {
                Email = TestEmail,
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = TestName
            };
            var response = await client.PostAsync(Route.Host + Route.BaseUrl + Route.Authentication + "/registration", JsonContent.Create(request));
            bool actual = response.IsSuccessStatusCode, expected = true;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task TestAccountRegistrationSuccessfully() {
            HttpClient client = new HttpClient();
            var request = new EmailPasswordRegistrationRequest {
                Email = TestEmail,
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = TestName
            };
            var response = await client.PostAsync(Route.Host + Route.BaseUrl + Route.Authentication + "/registration", JsonContent.Create(request));
            HttpStatusCode actual = response.StatusCode, expected = HttpStatusCode.OK;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task TestAccountRegistrationEmailExist() {
            HttpClient client = new HttpClient();
            var request = new EmailPasswordRegistrationRequest {
                Email = TestEmail,
                Password = "HelloWorld12345!!",
                ConfirmedPassword = "HelloWorld12345!!",
                Name = TestName
            };
            var response = await client.PostAsync(Route.Host + Route.BaseUrl + Route.Authentication, JsonContent.Create(request));
            bool actual = response.IsSuccessStatusCode, expected = true;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}