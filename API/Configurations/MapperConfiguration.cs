using API.Converter;
using API.Dto.Requests;
using API.Dto.Responses;
using API.Entities;
using API.Enums;
using AutoMapper;

namespace API.Configurations {
    public class MapperConfiguration: Profile {
        public MapperConfiguration()
        {
            // user-related
            CreateMap<User, UserResponse>();

            //TODO: converter for topic ids, referencesId
            // article-related
            CreateMap<ArticleCreationRequest, Article>()
                .ForMember(article => article.Authors, mappingOptions => mappingOptions.ConvertUsing<AuthorIdsToAuthorsConverter, ICollection<int>?>(src => src.AuthorIds))
                .ForMember(article => article.Topics, mappingOptions => mappingOptions.ConvertUsing<TopicIdsToTopicsConverter, ICollection<int>?>(src => src.TopicIds))
                .ForMember(article => article.Status, config => config.MapFrom(src => ArticleStatus.DRAFTED))
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ArticleCreationRequestText, Article>()
                .ForMember(article => article.Authors, mappingOptions => mappingOptions.ConvertUsing<AuthorIdsToAuthorsConverter, ICollection<int>?>(src => src.AuthorIds))
                .ForMember(article => article.Topics, mappingOptions => mappingOptions.ConvertUsing<TopicIdsToTopicsConverter, ICollection<int>?>(src => src.TopicIds))
                .ForMember(article => article.Status, config => config.MapFrom(src => ArticleStatus.DRAFTED))
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ArticleCreationRequestFile, Article>()
                .ForMember(article => article.Authors, mappingOptions => mappingOptions.ConvertUsing<AuthorIdsToAuthorsConverter, ICollection<int>?>(src => src.AuthorIds))
                .ForMember(article => article.Topics, mappingOptions => mappingOptions.ConvertUsing<TopicIdsToTopicsConverter, ICollection<int>?>(src => src.TopicIds))
                .ForMember(article => article.Status, config => config.MapFrom(src => ArticleStatus.DRAFTED))
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Article, ArticleResponse>()
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ArticleUpdateRequest, Article>()
                .ForAllMembers(configOptions => configOptions.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}