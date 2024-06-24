using API.Entities;

namespace API.Repositories {
    public interface ReferenceRepository: GenericRepository<Reference> {
    }

    public class ReferenceRepositoryImplementation : GenericRepositoryImplementation<Reference>, ReferenceRepository
    {
        public ReferenceRepositoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}