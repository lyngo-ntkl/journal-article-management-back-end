using API.Entities;

namespace API.Repositories {
    public interface ArticleRepository: GenericRepository<Article> {

    }
    public class ArticleRepositoryImplementation : GenericRepositoryImplementation<Article>, ArticleRepository
    {
        public ArticleRepositoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}