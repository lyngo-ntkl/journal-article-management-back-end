
namespace UseCases.Manuscripts.Get
{
    public class GetManuscriptsHandler : IGetManuscripts
    {
        public Task<ManuscriptResponse> GetManuscriptAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ManuscriptResponse>> GetManuscriptsForAuthorAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ManuscriptResponse>> GetManuscriptsForEditorAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ManuscriptResponse>> GetManuscriptsForReviewerAsync()
        {
            throw new NotImplementedException();
        }
    }
}
