using API.Entities;
using API.Repositories;
using AutoMapper;
using System.Linq.Expressions;
using System.Linq;
using API.Utils;

namespace API.Services
{
    public interface ReviewRequestService
    {
        public Task AssignReviewers(int assignArticleId);
    }

    public class ReviewRequestServiceImplementation : ReviewRequestService
    {
        private const int ReviewersPerPaper = 3;
        private const double ReferenceCoefficient = 0.6;
        private const double KeywordCoefficient = 0.25;
        private const double HIndexCoeffient = 0.15;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewRequestServiceImplementation(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AssignReviewers(int assignArticleId)
        {
            var article = await _unitOfWork.ArticleRepository.GetAsync(assignArticleId);
            if (article == null)
            {
                throw new Exception(ExceptionMessage.ArticleNotFound);
            }

            if (article.Status != ArticleStatus.SUBMITTED)
            {
                throw new Exception(ExceptionMessage.NotSubmittedArticle);
            }

            article.Status = ArticleStatus.REVIEWING;
            _unitOfWork.ArticleRepository.Update(article);
            await _unitOfWork.SaveAsync();

            //TODO: refactor

            #region get possible reviewers

            IEnumerable<User> possibleReviewers = null!;

            if (article.Topics != null &&  article.Topics.Any())
            {
                var topicIds = article.Topics.Select(topic => topic.Id);
                possibleReviewers = _unitOfWork.ArticleRepository.GetAll(
                    filter: a => a.Status == ArticleStatus.PUBLISHED && a.Topics.Any(t => topicIds.Contains(t.Id)),
                    includeProperties: nameof(Article.Authors))
                    .SelectMany(a => a.Authors)
                    .Distinct();
            }

            if (article.Authors != null && article.Authors.Any()) {
                var authorIds = article.Authors.Select(a => a.Id);
                possibleReviewers = possibleReviewers.Where(reviewer => !authorIds.Contains(reviewer.Id));
            }

            if (article.ReviewRequests  != null && article.ReviewRequests.Any())
            {
                var reviewerIds = article.ReviewRequests.Select(reviewer => reviewer.Id);
                possibleReviewers = possibleReviewers.Where(reviewer => !reviewerIds.Contains(reviewer.Id));
            }

            #endregion

            #region calculate priority & sort
            // TODO: research whether ML can use with this
            Dictionary<int, double> priorities = new Dictionary<int, double>();
            var referenceIds = article.References.Select(reference => reference.ReferenceArticleId);
            var keywordIds = article.Keywords.Select(a => a.Id);
            foreach (var reviewer in possibleReviewers)
            {
                // TODO: normalize H-index?
                double score = HIndexCoeffient * reviewer.HIndex!.Value
                    + ReferenceCoefficient * reviewer.Articles.Count(a => referenceIds.Contains(a.Id)) / referenceIds.Count()
                    + KeywordCoefficient * reviewer.Articles.SelectMany(a => a.Keywords).Distinct().Count(keyword => keywordIds.Contains(keyword.Id)) / keywordIds.Count();
                priorities[reviewer.Id] = score;
            }
            possibleReviewers.OrderByDescending(possibleReviewers => priorities[possibleReviewers.Id]);
            #endregion

            #region select reviewers & create request
            possibleReviewers = possibleReviewers.Take(ReviewersPerPaper);
            foreach (User reviewer in possibleReviewers)
            {
                await _unitOfWork.ReviewRequestRepository.InsertAsync(new ReviewRequest
                {
                    Article = article,
                    Reviewer = reviewer,
                    Status = ReviewRequestStatus.NOT_RESPONSE,
                    // TODO: datetime auto
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                });
                await _unitOfWork.SaveAsync();
                // TODO: notification
            }
            #endregion
        }
    }
}
