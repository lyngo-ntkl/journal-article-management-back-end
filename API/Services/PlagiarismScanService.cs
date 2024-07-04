using System.Text;
using API.Utils;
using Copyleaks.SDK.V3.API;
using Copyleaks.SDK.V3.API.Models.Requests.Properties;
using Copyleaks.SDK.V3.API.Models.Responses;
using Copyleaks.SDK.V3.API.Models.Types;

namespace API.Services {
    public interface PlagiarismService {
        Task Scan(string text, string fileName);
    }

    public class PlagiarismServiceImplementation: PlagiarismService {
        private readonly IConfiguration _configuration;
        private LoginResponse? _loginResponse;

        public PlagiarismServiceImplementation(IConfiguration configuration)
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
                        Status = new Uri($"https://localhost:7258{ApiPath.Version1}{ApiPath.Api}{ApiPath.Articles}/plagiarism")
                    },
                    Sandbox = true
                }
            };
            await scanner.SubmitFileAsync(scanId, request, _loginResponse?.Token);
        }
    }
}