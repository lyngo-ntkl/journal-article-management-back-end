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
        Task<ArticleResponse?> CreateNewArticle(ArticleCreationRequest request);
        Task<ArticleResponse?> UpdateArticle(int articleId, ArticleUpdateRequest request);
        Task<ArticleResponse?> GetArticle(int articleId);
        Task<ArticleResponse> DeleteDraftArticle(int articleId);
        Task<ArticleResponse> DeleteDraftArticlePermanent(int articleId);
        Task<ArticleResponse> SubmitArticle(int articleId);
    }
    public class ArticleServiceImplementation : ArticleService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FileConverter _fileConverter;
        public ArticleServiceImplementation(UnitOfWork unitOfWork, IMapper mapper, FileConverter fileConverter) {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._fileConverter = fileConverter;
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

            // TODO: hard delete after 30 days of soft delete
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
                var article = await GetArticleEntity(articleId);
                article = _unitOfWork.ArticleRepository.Update(_mapper.Map(request, article));
                return _mapper.Map<ArticleResponse>(article);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        private async Task<Article> CreateNewArticleByFile(ArticleCreationRequest request) {
            //TODO: file convertion & file upload
            var fileRequest = (ArticleCreationRequestFile) request;
            var file = fileRequest.File;
            
            return await _unitOfWork.ArticleRepository.InsertAsync(_mapper.Map<Article>(request));
        }

        private async Task<Article> CreateNewArticleByText(ArticleCreationRequest request) {
            Article article = _mapper.Map<Article>(request);
            article = await _unitOfWork.ArticleRepository.InsertAsync(article);
            await _unitOfWork.SaveAsync();
            return article;
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
    }
}