using AutoMapper;
using Entities;
using UseCases.Articles.Create;
namespace Infrastructure.Automapper
{
    public class ArticleMappingProfile: Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<ArticleSubmissionRequest, Article>();
        }
    }
}
