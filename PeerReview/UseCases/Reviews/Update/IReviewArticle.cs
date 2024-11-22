namespace UseCases.Reviews.Update
{
    public interface IReviewArticle
    {
        void UpdateDraftReviewAsync(ReviewUpdateRequest request);
        void SubmitReviewAsync(int id);
    }
}
