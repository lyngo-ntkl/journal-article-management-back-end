namespace UseCases.Articles.Create
{
    public interface ISubmitArticle
    {
        Task<int> SubmitArticleAsync(ArticleSubmissionRequest request);
    }
}
