using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text.RegularExpressions;
using API.CronJob;
using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Enums;
using API.Repositories;
using API.Utils;
using AutoMapper;
using Quartz;
using Quartz.Impl;

namespace API.Services {
    public interface ArticleService {
        Task<ArticleResponse?> CreateNewArticleByText(ArticleCreationRequestText request);
        Task<ArticleResponse?> CreateNewArticleByFile(ArticleCreationRequestFile request);
        Task<ArticleResponse?> UpdateArticle(int articleId, ArticleUpdateRequest request);
        Task<Collection<ArticleResponse>> GetArticles();
        Task<Collection<ArticleResponse>> GetPersonalArticles();
        Task<ArticleResponse?> GetArticle(int articleId);
        Task<ArticleResponse> DeleteDraftArticle(int articleId);
        Task<ArticleResponse> DeleteDraftArticlePermanent(int articleId);
        Task<ArticleResponse> SubmitArticle(int articleId);
    }
    public class ArticleServiceImplementation : ArticleService
    {
        private const string ArticleStructurePattern = "(?:Abstract|Abstraction)(?:\n|\r\n)((?:.|\n|\r\n)*)Introduction(?:\n|\r\n)((?:.|\n|\r\n)*)(?:Methods|Methodology)(?:\n|\r\n)((?:.|\n|\r\n)*)Results(?:\n|\r\n)((?:.|\n|\r\n)*)Conclusion(?:\n|\r\n)((?:.|\n|\r\n)*)References(?:\n|\r\n)((?:.|\n|\r\n)*)";
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FileConverter _fileConverter;
        private readonly FirebaseStorageService _firebaseStorageService;
        private readonly JwtUtils _jwtUtils;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleServiceImplementation(UnitOfWork unitOfWork, IMapper mapper, FileConverter fileConverter, FirebaseStorageService firebaseStorageService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._fileConverter = fileConverter;
            this._firebaseStorageService = firebaseStorageService;
            this._jwtUtils = new JwtUtils(configuration);
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<ArticleResponse?> CreateNewArticleByFile(ArticleCreationRequestFile request) {
            //TODO: file convertion & file upload
            var file = request.File;
            if (file == null) {
                throw new Exception(ExceptionMessage.FileNotExist);
            }
            
            var content = await _fileConverter.ConvertFileToText(file);
            var match = Regex.Match(content, ArticleStructurePattern);
            if (match == null || !match.Success) {
                throw new Exception(ExceptionMessage.WrongFileFormat);
            }
            var groups = match.Groups;
            var article = new Article {
                Title = content.Split("Abstract", 2)[0].Trim(),
                Abstract = groups[1].Value.Trim(),
                Introduction = groups[2].Value.Trim(),
                Method = groups[3].Value.Trim(),
                Results = groups[4].Value.Trim(),
                Conclusion = groups[5].Value.Trim()
            };
            

            // article.FilePath = await _firebaseStorageService.UploadFileAsync(file.OpenReadStream(), file.ContentType, file.FileName);

            article = await _unitOfWork.ArticleRepository.InsertAsync(article);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ArticleResponse>(article);
        }

        public async Task<ArticleResponse?> CreateNewArticleByText(ArticleCreationRequestText request) {
            try {
                Article article = _mapper.Map<Article>(request);
                article = await _unitOfWork.ArticleRepository.InsertAsync(article);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<ArticleResponse>(article);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<ArticleResponse> DeleteDraftArticle(int articleId)
        {
            var article = await GetArticleEntity(articleId);
            if (article.Status != ArticleStatus.DRAFTED) {
                throw new Exception(ExceptionMessage.DraftArticleDeletionAllowance);
            }
            // if (article.IsDeleted) {
            //     throw new Exception("Can not delete the deleted draft article");
            // }
            var deletedDate = DateTime.UtcNow;
            article.IsDeleted = true;
            article.DeletedDate = deletedDate;
            article = _unitOfWork.ArticleRepository.Update(article);
            await _unitOfWork.SaveAsync();

            // TODO: hard delete after 30 days of soft delete
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            // TODO: using factory later
            PermanentDeletionJob.ArticleService = this;
            IJobDetail job = JobBuilder.Create<PermanentDeletionJob>()
                .UsingJobData("id", articleId)
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(deletedDate.AddDays(30))
                .Build();
            await scheduler.ScheduleJob(job, trigger);
            return _mapper.Map<ArticleResponse>(article);
        }

        public async Task<ArticleResponse> DeleteDraftArticlePermanent(int articleId)
        {
            var article = await GetArticleEntity(articleId);
            if (article.Status != ArticleStatus.DRAFTED) {
                throw new Exception(ExceptionMessage.DraftArticleDeletionAllowance);
            }
            if (! article.IsDeleted) {
                throw new Exception(ExceptionMessage.SoftDeletion);
            }
            _unitOfWork.ArticleRepository.Delete(article);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ArticleResponse>(article);
        }

        public async Task<ArticleResponse?> GetArticle(int articleId)
        {
            return _mapper.Map<ArticleResponse?>(await GetArticleEntity(articleId));
        }

        private async Task<Article> GetArticleEntity(int articleId)
        {
            var article = await _unitOfWork.ArticleRepository.GetAsync(articleId);
            if (article == null) {
                // TODO: handle using Global handler
                throw new Exception(ExceptionMessage.ArticleNotFound);
            }
            return article;
        }

        public async Task<ArticleResponse?> UpdateArticle(int articleId, ArticleUpdateRequest request)
        {
            try {
                var id = int.Parse(_jwtUtils.GetSidClaim(_httpContextAccessor)!);
                if (request.AuthorIds != null && !request.AuthorIds.Contains(id)) {
                    throw new Exception(ExceptionMessage.UnauthorityToModifyArticle);
                }

                // TODO: History. Should remove once the article is published/approved
                var article = await GetArticleEntity(articleId);

                if (article.Status != ArticleStatus.DRAFTED &&
                    article.Status != ArticleStatus.MINOR_REVISION &&
                    article.Status != ArticleStatus.MAJOR_REVISION) {
                    throw new Exception(ExceptionMessage.UnableToEdit);
                }

                article = _unitOfWork.ArticleRepository.Update(_mapper.Map(request, article));
                return _mapper.Map<ArticleResponse>(article);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<ArticleResponse> SubmitArticle(int articleId)
        {
            var article = await GetArticleEntity(articleId);

            if (article.Status != ArticleStatus.DRAFTED &&
                article.Status != ArticleStatus.MINOR_REVISION &&
                article.Status != ArticleStatus.MAJOR_REVISION) {
                throw new Exception(ExceptionMessage.UnableToSubmit);
            }

            if (string.IsNullOrWhiteSpace(article.Title) ||
                string.IsNullOrWhiteSpace(article.Abstract) ||
                string.IsNullOrWhiteSpace(article.Introduction) ||
                string.IsNullOrWhiteSpace(article.Method) ||
                string.IsNullOrWhiteSpace(article.Results) ||
                string.IsNullOrWhiteSpace(article.Conclusion) ||
                article.Authors == null || article.Authors.Count() == 0 ||
                article.References == null || article.References.Count() == 0) {
                throw new Exception(ExceptionMessage.UnqualifiedSubmission);
            }

            article.Status = ArticleStatus.SUBMITTED;
            article = _unitOfWork.ArticleRepository.Update(article);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ArticleResponse>(article);
        }

        public async Task<Collection<ArticleResponse>> GetArticles()
        {
            return _mapper.Map<Collection<ArticleResponse>>(await _unitOfWork.ArticleRepository.GetAllAsync(includeProperties: $"{nameof(Article.Authors)}"));
        }

        public async Task<Collection<ArticleResponse>> GetPersonalArticles()
        {
            var id = int.Parse(_jwtUtils.GetSidClaim(_httpContextAccessor)!);
            var user = await _unitOfWork.UserRepository.GetAsync(id);

            // TODO: expression tree
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync(includeProperties: $"{nameof(Article.Authors)}");
            articles = articles.FindAll(article => article.Authors?.FirstOrDefault(author => author.Id == id) != null);
            return _mapper.Map<Collection<ArticleResponse>>(articles);
        }
    }
}