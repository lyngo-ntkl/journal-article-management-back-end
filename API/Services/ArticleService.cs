using API.Dto.Requests;
using API.Repositories;

namespace API.Services {
    public interface ArticleService {
        Task CreateNewArticle(ArticleCreationRequest request);
    }
    public class ArticleServiceImplementation : ArticleService
    {
        private UnitOfWork _unitOfWork;
        public ArticleServiceImplementation(UnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public Task CreateNewArticle(ArticleCreationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}