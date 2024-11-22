using UseCases.ReviewRequests.Update;
using Infrastructure.gRPC.Protos;
using Grpc.Core;

namespace Infrastructure.gRPC.Services
{
    public class ReviewRequestApi(
        IAcceptReviewRequest acceptReviewRequest,
        IRejectReviewRequest rejectReviewRequest
        ): ReviewRequestAPI.ReviewRequestAPIBase
    {
        private readonly IAcceptReviewRequest _acceptReviewRequest = acceptReviewRequest;
        private readonly IRejectReviewRequest _rejectReviewRequest = rejectReviewRequest;

        public override Task<GrpcReviewRequestResponse> AcceptReviewRequest(GrpcReviewRequestId request, ServerCallContext context)
        {
            return base.AcceptReviewRequest(request, context);
        }

        public override Task<GrpcReviewRequestResponse> RejectReviewRequest(GrpcReviewRequestId request, ServerCallContext context)
        {
            return base.RejectReviewRequest(request, context);
        }
    }
}
