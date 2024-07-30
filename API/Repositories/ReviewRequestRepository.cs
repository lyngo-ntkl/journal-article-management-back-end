using API.Entities;
namespace API.Repositories
{
    public interface ReviewRequestRepository: GenericRepository<ReviewRequest> {
        
    }

    public class ReviewRequestRepositoryImplementation : GenericRepositoryImplementation<ReviewRequest>, ReviewRequestRepository
    {
        public ReviewRequestRepositoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}