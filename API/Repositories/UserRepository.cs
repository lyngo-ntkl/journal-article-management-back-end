using API.Entities;

namespace API.Repositories {
    public interface UserRepository: GenericRepository<User> {
    }

    public class UserRepositoryImplementation : GenericRepositoryImplementation<User>, UserRepository
    {
        public UserRepositoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}