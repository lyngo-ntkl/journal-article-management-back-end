using API.Controllers;

namespace UnitTesting.Article {
    public class ArticleCreationTest {
        private const string BaseUrl = "https://localhost:7258";
        [SetUp]
        public void SetUp() {
            // _articleContoller = new ArticleController();
        }

        [Test]
        public async Task TestUploadFileToFireBaseCloudStorage() {
            HttpClient client = new HttpClient();
            var fileStream = File.OpenRead("");
            MultipartFormDataContent body = new MultipartFormDataContent() {
                new StreamContent(fileStream)
            };
            var response = await client.PostAsync(BaseUrl, body);
            var url = await response.Content.ReadAsStringAsync();
            Console.WriteLine(url);
            Assert.IsNotEmpty(url);
        }
    }
}