using System.Text;
using Copyleaks.SDK.V3.API;
using Copyleaks.SDK.V3.API.Exceptions;
using Copyleaks.SDK.V3.API.Models.Requests.Properties;
using Copyleaks.SDK.V3.API.Models.Responses;
using Copyleaks.SDK.V3.API.Models.Responses.Result;
using Copyleaks.SDK.V3.API.Models.Types;

namespace API.Services {
    public interface CopyleaksPlagiarismService {
        Task Scan(string text, string fileName);
        Task ProcessResult(string status, string scanId, Result request);
    }

    public class CopyleaksPlagiarismServiceImplementation: CopyleaksPlagiarismService {
        private readonly IConfiguration _configuration;
        private LoginResponse? _loginResponse;

        public CopyleaksPlagiarismServiceImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task AuthenticateCopyleaks() {
            if (_loginResponse == null || _loginResponse.Expire.ToUniversalTime() < DateTime.UtcNow) {
                var httpClient = new HttpClient();
                var identityClient = new CopyleaksIdentityApi(httpClient);
                _loginResponse = await identityClient.LoginAsync(_configuration["copyleaks:email"], _configuration["copyleaks:key"]);
            }
        }

        public async Task Scan(string text, string fileName) {
            try {
                await AuthenticateCopyleaks();
                var httpClient = new HttpClient();
                var scanner = new CopyleaksScansApi(httpClient);
                var scanId = Guid.NewGuid().ToString();
                var request = new Copyleaks.SDK.V3.API.Models.Requests.FileDocument{
                    Base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text)),
                    Filename = fileName,
                    PropertiesSection = new ClientScanProperties() {
                        Action = eSubmitAction.Scan,
                        Webhooks = new Webhooks {
                            Status = new Uri(_configuration["host"] + "/copyleaks/plagiarism/{status}/" + scanId)
                        },
                        Sandbox = true
                    }
                };
                await scanner.SubmitFileAsync(scanId, request, _loginResponse?.Token);
            } catch (CopyleaksHttpException e) {
                if (e.HttpErrorCode == System.Net.HttpStatusCode.Conflict) {
                    await Scan(text, fileName);
                } else {
                    throw;
                }
            }
        }

        public Task ProcessResult(string status, string scanId, Result request)
        {
            switch (status) {
                case "completed":
                    break;
                case "error":
                    break;
                case "creditsChecked":
                    break;
                default:
                    break;
            }
            throw new NotImplementedException();
        }
    }

    public class PlagiarismServiceImplementation
    {
        public Task Scan(string text, string fileName)
        {

            throw new NotImplementedException();
        }
    }
}