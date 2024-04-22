using API.Entities;

namespace API.Repositories {
    public interface UnitOfWork {
        int Save();
        Task<int> SaveAsync();
        UserRepository UserRepository {get;}
        ArticleRepository ArticleRepository {get;}
    }

    public class UnitOfWorkImplementation : UnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly ArticleRepository _articleRepository;

        public UnitOfWorkImplementation(ApplicationDbContext dbContext, UserRepository userRepository, ArticleRepository articleRepository)
        {
            this._dbContext = dbContext;
            this._userRepository = userRepository;
            this._articleRepository = articleRepository;
        }

        public UserRepository UserRepository => _userRepository;

        public ArticleRepository ArticleRepository => _articleRepository;

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