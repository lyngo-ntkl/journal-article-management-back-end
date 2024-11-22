using Grpc.Core;
using Infrastructure.gRPC.Protos;
using UseCases.Manuscripts.Create;
using UseCases.Common;
using UseCases.Manuscripts.Get;
namespace Infrastructure.gRPC.Services
{
    public class ManuscriptApiImpl(
        ISubmitManuscript submitManuscript,
        IGetManuscripts getManuscripts,
        IMapping mapping) : ManuscriptAPI.ManuscriptAPIBase
    {
        private readonly ISubmitManuscript _submitManuscript = submitManuscript;
        private readonly IGetManuscripts _getManuscripts = getManuscripts;
        private readonly IMapping _mapping = mapping;

        /// <summary>
        /// Get an manuscript content by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GrpcManuscriptResponse> GetManuscript(GrpcGetManuscriptRequest request, ServerCallContext context)
        {
            return base.GetManuscript(request, context);
        }

        /// <summary>
        /// Get all manuscripts of author
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GrpcGetManuscriptsResponse> GetManuscriptsForAuthor(GrpcGetManuscriptsRequest request, ServerCallContext context)
        {
            return base.GetManuscriptsForAuthor(request, context);
        }

        /// <summary>
        /// Get all manuscripts
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GrpcGetManuscriptsResponse> GetManuscriptsForEditor(GrpcGetManuscriptsRequest request, ServerCallContext context)
        {
            return base.GetManuscriptsForEditor(request, context);
        }

        /// <summary>
        /// Get all manuscripts that reviewer need to review
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GrpcGetManuscriptsResponse> GetManuscriptsForReviewer(GrpcGetManuscriptsRequest request, ServerCallContext context)
        {
            return base.GetManuscriptsForReviewer(request, context);
        }

        /// <summary>
        /// Submit an manuscript
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GrpcSubmitManuscriptResponse> SubmitManuscript(GrpcSubmitManuscriptRequest request, ServerCallContext context)
        {
            var id = await _submitManuscript.SubmitManuscriptAsync(_mapping.Map<ManuscriptSubmissionRequest>(request));
            return new GrpcSubmitManuscriptResponse
            {
                Id = id
            };
        }
    }
}
