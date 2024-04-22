using API.Dto.Requests;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Services {
    public interface ArticleService {
        Task CreateNewArticle(ArticleCreationRequest request);
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
                await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}