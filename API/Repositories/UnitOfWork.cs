using API.Entities;

namespace API.Repositories {
    public interface UnitOfWork {
        int Save();
        Task<int> SaveAsync();
        UserRepository UserRepository {get;}
    }

    public class UnitOfWorkImplementation : UnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepository;

        public UnitOfWorkImplementation(ApplicationDbContext dbContext, UserRepository userRepository)
        {
            _dbContext = dbContext;
            this._userRepository = userRepository;
        }

        public UserRepository UserRepository => _userRepository;

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