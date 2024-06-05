using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Enums;
using API.Repositories;
using AutoMapper;

namespace API.Services {
    public interface ArticleService {
        Task<ArticleResponse?> CreateNewArticle(ArticleCreationRequest request);
        Task<ArticleResponse?> UpdateArticle(int articleId, ArticleUpdateRequest request);
        Task<ArticleResponse?> GetArticle(int articleId);
        Task<ArticleResponse> DeleteDraftArticle(int articleId);
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

        public async Task<ArticleResponse> DeleteDraftArticle(int articleId)
        {
            // TODO: soft delete first, hard delete after 30 days of soft delete
            var article = await _unitOfWork.ArticleRepository.GetAsync(articleId);
            if (article == null) {
                throw new Exception("Article not found");
            }
            if (article.Status != ArticleStatus.DRAFTED) {
                throw new Exception("Only draft article can be delete");
            }
            article.IsDeleted = true;
            article.DeletedDate = DateTime.UtcNow;
            article = _unitOfWork.ArticleRepository.Update(article);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ArticleResponse>(article);
        }

        public async Task<ArticleResponse?> GetArticle(int articleId)
        {
            var article = await _unitOfWork.ArticleRepository.GetAsync(articleId);
            if (article == null) {
                throw new Exception("Article not found");
            }
            return _mapper.Map<ArticleResponse?>(article);
        }

        public async Task<ArticleResponse?> UpdateArticle(int articleId, ArticleUpdateRequest request)
        {
            try {
                Article? article = await _unitOfWork.ArticleRepository.GetAsync(articleId);
                if (article == null) {
                    // TODO: handle using Global handler
                    throw new Exception("Article not found");
                }
                article = _unitOfWork.ArticleRepository.Update(_mapper.Map(request, article));
                return _mapper.Map<ArticleResponse>(article);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        private async Task<Article> CreateNewArticleByFile(ArticleCreationRequest request) {
            //TODO: file convertion & file upload
            return await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
        }

        private async Task<Article> CreateNewArticleByText(ArticleCreationRequest request) {
            Article article = _mapper.Map<Article>(request);
            article = await _unitOfWork.ArticleRepository.InsertAsync(article);
            await _unitOfWork.SaveAsync();
            return article;
        }
    }
}