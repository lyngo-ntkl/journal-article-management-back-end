using AutoMapper;
using Entities;
using UseCases.ReviewRequests.Get;

namespace Infrastructure.Automapper
{
    public class ReviewRequestMappingProfile: Profile
    {
        public ReviewRequestMappingProfile()
        {
            CreateMap<ReviewRequest, ReviewRequestResponse>();
        }
    }
}
