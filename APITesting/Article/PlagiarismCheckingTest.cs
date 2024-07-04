using API.Services;
using Constant;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTesting.Article {
    public class PlagiarismCheckingTest {
        private PlagiarismService _plagiarismService;
        [SetUp]
        public void SetUp() {
            // Arrange
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(config => config["copyleaks:email"]).Returns(Key.CopyleaksEmail);
            configuration.Setup(config => config["copyleaks:key"]).Returns(Key.CopyleaksKey);
            _plagiarismService = new PlagiarismServiceImplementation(configuration.Object);
        }

        // [Test]
        // public void Scan_NonPlagiarizedDocument_Success() {

        //     // Act
        //     _plagiarismService.Scan();

        //     //Assert
        // }
    }
}