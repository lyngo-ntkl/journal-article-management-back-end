namespace UseCases.Manuscripts.Get
{
    public interface IGetManuscripts
    {
        Task<ManuscriptResponse> GetManuscriptAsync();
        Task<ICollection<ManuscriptResponse>> GetManuscriptsForAuthorAsync();
        Task<ICollection<ManuscriptResponse>> GetManuscriptsForEditorAsync();
        Task<ICollection<ManuscriptResponse>> GetManuscriptsForReviewerAsync();
    }
}
