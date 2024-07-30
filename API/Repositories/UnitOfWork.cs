using API.Entities;

namespace API.Repositories {
    public interface UnitOfWork {
        int Save();
        Task<int> SaveAsync();
        UserRepository UserRepository {get;}
        ArticleRepository ArticleRepository {get;}
        TopicRepository TopicRepository {get;}
        ReferenceRepository ReferenceRepository {get;}
        ReviewRequestRepository ReviewRequestRepository { get;}
    }

    public class UnitOfWorkImplementation : UnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly ArticleRepository _articleRepository;
        private readonly TopicRepository _topicRepository;
        private readonly ReferenceRepository _referenceRepository;
        private readonly ReviewRequestRepository _reviewRequestRepository;

        public UnitOfWorkImplementation(ApplicationDbContext dbContext, UserRepository userRepository,
            ArticleRepository articleRepository, TopicRepository topicRepository, ReferenceRepository referenceRepository, ReviewRequestRepository reviewRequestRepository)
        {
            this._dbContext = dbContext;
            this._userRepository = userRepository;
            this._articleRepository = articleRepository;
            this._topicRepository = topicRepository;
            this._referenceRepository = referenceRepository;
            _reviewRequestRepository = reviewRequestRepository;
        }

        public UserRepository UserRepository => _userRepository;

        public ArticleRepository ArticleRepository => _articleRepository;

        public TopicRepository TopicRepository => _topicRepository;

        public ReferenceRepository ReferenceRepository => _referenceRepository;

        public ReviewRequestRepository ReviewRequestRepository => _reviewRequestRepository;

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