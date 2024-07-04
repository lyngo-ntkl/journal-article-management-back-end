using Constant;
using Moq;

namespace UnitTesting.Articles {
    public class ArticleFetchingTest {
        private TestingServiceSetUp _setup;

        [SetUp]
        public void SetUp() {
            _setup = new TestingServiceSetUp();
            // _setup.UnitOfWork.Setup(uow => uow.ArticleRepository.GetAsync(1))
            //     .ReturnsAsync(new API.Entities.Article {
            //         Id = 1,
            //         Abstract = "Mock abstract",
            //         Introduction = "Mock introduction",
            //         Method = "Mock method",
            //         Results = "Mock result",

            //     });
            // _setup.UnitOfWork.Setup(uow => uow.ArticleRepository.GetAllAsync)
        }

        [Test]
        public async Task TestGetArticlesSuccessfully() {
            
        }

        [Test]
        public async Task TestGetArticleSuccessfully() {
            
        }

        [Test]
        public async Task TestGetPersonalArticlesSuccessfully() {
            
        }
    }
}