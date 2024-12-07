namespace UseCases.Reviews.Update
{
    public interface IReviewManuscript
    {
        Task UpdateDraftReviewAsync(ReviewUpdateRequest request);
        Task SubmitReviewAsync(int id);
    }
}
