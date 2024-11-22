using AutoMapper;
using UseCases.Common;

namespace Infrastructure.Automapper
{
    public class MappingImpl(IMapper mapper) : IMapping
    {
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }
    }
}
