using System.Net.Http.Headers;
using System.Net.Http.Json;
using API.Controllers;
using API.Dto.Requests;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UnitTesting.Article {
    public class ArticleCreationTest {
        // private ArticleController _articleController;
        private const string BaseUrl = "https://localhost:7258/v1/api/articles";

        [SetUp]
        public void SetUp() {
            // _articleController = new ArticleController();
        }

        [Test]
        public async Task TestCreateArticleUsingTextSuccefully() {
            HttpClient client = new HttpClient();
            ArticleCreationRequestText request = new ArticleCreationRequestText() {
                Abstract = "",
                Introduction = "",
                Method = "",
                Results = "",
                ReferenceIds = {},
                AuthorIds = {},
                TopicIds = {}
            };
            JsonContent body = JsonContent.Create(request);
            await client.PostAsync(BaseUrl, body);
        }
        
        [Test]
        public async Task TestCreateArticleUsingFileSuccessfully() {
            ArticleCreationRequestFile request = new ArticleCreationRequestFile() {
                File = new MultipartContent(),
                AuthorIds = {},
                TopicIds = {}
            };
        }
    }
}