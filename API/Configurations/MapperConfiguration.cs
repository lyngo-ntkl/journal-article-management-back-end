using API.Converter;
using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using AutoMapper;

namespace API.Configurations {
    public class MapperConfiguration: Profile {
        public MapperConfiguration(AuthorIdsToAuthorsConverter authorIdsToAuthorsConverter)
        {
            CreateMap<User, UserResponse>();
            CreateMap<ArticleCreationRequest, Article>()
                .ForMember(article => article.Authors, mappingOptions => mappingOptions.ConvertUsing(authorIdsToAuthorsConverter, src => src.AuthorIds))
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}