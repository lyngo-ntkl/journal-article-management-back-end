namespace UseCases.Manuscripts.Create
{
    public interface ISubmitManuscript
    {
        Task<int> SubmitManuscriptAsync(ManuscriptSubmissionRequest request);
    }
}
