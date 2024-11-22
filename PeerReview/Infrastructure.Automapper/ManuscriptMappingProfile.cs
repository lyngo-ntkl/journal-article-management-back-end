using AutoMapper;
using Entities;
using UseCases.Manuscripts.Create;
namespace Infrastructure.Automapper
{
    public class ManuscriptMappingProfile: Profile
    {
        public ManuscriptMappingProfile()
        {
            CreateMap<ManuscriptSubmissionRequest, Manuscript>();
        }
    }
}
