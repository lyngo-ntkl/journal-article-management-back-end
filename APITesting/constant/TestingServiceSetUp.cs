using API.Configurations;
using API.Entities;
using API.Repositories;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Constant {
    public class TestingServiceSetUp {
        public Mock<UnitOfWork> UnitOfWork { get; private set; }
        public IMapper Mapper { get; private set; }
        public Mock<IHttpContextAccessor> HttpContextAccessor { get; private set; }
        public Mock<IConfiguration> Configuration { get; private set; }
        public FileConverter FileConverter { get; private set; }
        public FirebaseStorageService FirebaseStorageService { get; private set; }
        public FirebaseConfiguration FirebaseConfiguration { get; private set; }
        public UserService UserService { get; private set; }
        public ArticleService ArticleService { get; private set; }

        public TestingServiceSetUp()
        {
            UnitOfWork = new Mock<UnitOfWork>();
            Mapper = new Mapper(new MapperConfiguration(config => config.AddProfile<MapperProfile>()));
            Configuration = new Mock<IConfiguration>();
            HttpContextAccessor = new Mock<IHttpContextAccessor>();
            FirebaseConfiguration = new FirebaseConfiguration(Configuration.Object);

            // supported service
            FileConverter = new FileConverterImplementation();
            FirebaseStorageService = new FirebaseStorageServiceImplementation(FirebaseConfiguration);

            // main service
           UserService = new UserServiceImplementation(UnitOfWork.Object, Configuration.Object, Mapper, HttpContextAccessor.Object);
           ArticleService = new ArticleServiceImplementation(UnitOfWork.Object, Mapper, FileConverter, FirebaseStorageService, Configuration.Object, HttpContextAccessor.Object);
        }
    }
}