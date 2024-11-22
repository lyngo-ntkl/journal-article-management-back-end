using Grpc.Core;
using Infrastructure.gRPC.Protos;
using UseCases.Articles.Create;
using UseCases.Common;
namespace Infrastructure.gRPC.Services
{
    public class ArticleApi(ISubmitArticle submitArticle, IMapping mapping) : ArticlesAPI.ArticlesAPIBase
    {
        private readonly ISubmitArticle _submitArticle = submitArticle;
        private readonly IMapping _mapping = mapping;

        public override Task<GrpcArticleResponse> GetArticle(GrpcGetArticleRequest request, ServerCallContext context)
        {
            return base.GetArticle(request, context);
        }

        public override async Task<GrpcSubmitArticleResponse> SubmitArticle(GrpcSubmitArticleRequest request, ServerCallContext context)
        {
            var id = await _submitArticle.SubmitArticleAsync(_mapping.Map<ArticleSubmissionRequest>(request));
            return new GrpcSubmitArticleResponse
            {
                Id = id
            };
        }
    }
}
