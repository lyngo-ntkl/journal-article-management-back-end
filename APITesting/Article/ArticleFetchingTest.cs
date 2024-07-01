using API.Repositories;
using API.Services;
using Moq;

namespace UnitTesting.Article {
    public class ArticleFetchingTest {
        private UserService _userService;
        private ArticleService _articleService;

        [SetUp]
        public void SetUp() {
            var unitOfWork = new Mock<UnitOfWork>();
            unitOfWork.Setup(uow => uow.ArticleRepository.GetAsync(1))
                .ReturnsAsync(new API.Entities.Article {
                    Id = 1,
                    Abstract = "Mock abstract",
                    Introduction = "Mock introduction",
                    Method = "Mock method",
                    Results = "Mock result",

                });
        }

        [Test]
        public async Task TestGetArticles() {
            
        }
    }
}