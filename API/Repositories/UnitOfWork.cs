using API.Entities;

namespace API.Repositories {
    public interface UnitOfWork {
        int Save();
        Task<int> SaveAsync();
        UserRepository UserRepository {get;}
        ArticleRepository ArticleRepository {get;}
        TopicRepository TopicRepository {get;}
    }

    public class UnitOfWorkImplementation : UnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly ArticleRepository _articleRepository;
        private readonly TopicRepository _topicRepository;

        public UnitOfWorkImplementation(ApplicationDbContext dbContext, UserRepository userRepository, 
            ArticleRepository articleRepository, TopicRepository topicRepository)
        {
            this._dbContext = dbContext;
            this._userRepository = userRepository;
            this._articleRepository = articleRepository;
            this._topicRepository = topicRepository;
        }

        public UserRepository UserRepository => _userRepository;

        public ArticleRepository ArticleRepository => _articleRepository;

        public TopicRepository TopicRepository => _topicRepository;

        public int Save()
        {
            return this._dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this._dbContext.SaveChangesAsync();
        }
    }
}