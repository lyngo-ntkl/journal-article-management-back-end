using API.Dto.Requests;
using API.Entities;
using Constant;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTesting.Articles {
    public class ArticleUpdateTest {
        private TestingServiceSetUp _setup;

        [SetUp]
        public void SetUp() {
            _setup = new TestingServiceSetUp();
            _setup.ApplicationDbContext.AddRange(MockData.DraftArticles);
            _setup.ApplicationDbContext.SaveChanges();
        }

        [Test]
        public async Task UpdateArticle_Text_Success() {
            var request = new ArticleUpdateRequest {
                Abstract = "Updated abstract",
                Introduction = "Updated introduction",
                Method = "Updated methods",
                Results = "Updated results",
                Conclusion = "Updated conclusion",
                AuthorIds = [1, 2, 3],
                TopicIds = [1],
                ReferenceIds = [1]
            };
            var actual = await _setup.ArticleService.UpdateArticle(1, request);
            Assert.NotNull(actual);
        }
    }
}