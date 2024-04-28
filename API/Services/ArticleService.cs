using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Services {
    public interface ArticleService {
        Task<ArticleResponse?> CreateNewArticle(ArticleCreationRequest request);
        Task<ArticleResponse?> UpdateArticle();
    }
    public class ArticleServiceImplementation : ArticleService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleServiceImplementation(UnitOfWork unitOfWork, IMapper mapper) {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ArticleResponse?> CreateNewArticle(ArticleCreationRequest request)
        {
            try {
                Article article;
                if (request.GetType() == typeof(ArticleCreationRequestFile)) {
                    article = await CreateNewArticleByFile(request);
                } else {
                    article = await CreateNewArticleByText(request);
                }
                return _mapper.Map<ArticleResponse>(article);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public Task<ArticleResponse?> UpdateArticle()
        {
            try {

            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            throw new NotImplementedException();
        }

        private async Task<Article> CreateNewArticleByFile(ArticleCreationRequest request) {
            //TODO: file convertion & file upload
            return await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
        }

        private async Task<Article> CreateNewArticleByText(ArticleCreationRequest request) {
            return await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
        }
    }
}