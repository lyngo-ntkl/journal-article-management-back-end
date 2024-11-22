namespace UseCases.Reviews.Update
{
    public interface IReviewArticle
    {
        Task UpdateDraftReviewAsync(ReviewUpdateRequest request);
        Task SubmitReviewAsync(int id);
    }
}
