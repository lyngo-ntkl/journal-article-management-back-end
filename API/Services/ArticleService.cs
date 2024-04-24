using API.Dto.Requests;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Services {
    public interface ArticleService {
        Task CreateNewArticle(ArticleCreationRequest request);
        Task UpdateArticle();
    }
    public class ArticleServiceImplementation : ArticleService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleServiceImplementation(UnitOfWork unitOfWork, IMapper mapper) {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task CreateNewArticle(ArticleCreationRequest request)
        {
            try {
                if (request.GetType() == typeof(ArticleCreationRequestFile)) {
                    await CreateNewArticleByFile(request);
                } else {
                    await CreateNewArticleByText(request);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Task UpdateArticle()
        {
            throw new NotImplementedException();
        }

        private async Task CreateNewArticleByFile(ArticleCreationRequest request) {
            //TODO: file convertion & file upload
            await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
            throw new NotImplementedException();
        }

        private async Task CreateNewArticleByText(ArticleCreationRequest request) {
            await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
        }
    }
}