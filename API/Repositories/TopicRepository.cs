using API.Entities;

namespace API.Repositories {
    public interface TopicRepository: GenericRepository<Topic> {}

    public class TopicRepositoryImplementation : GenericRepositoryImplementation<Topic>, TopicRepository
    {
        public TopicRepositoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}