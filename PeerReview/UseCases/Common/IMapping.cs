namespace UseCases.Common
{
    public interface IMapping
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        TDestination Map<TDestination>(object source);
    }
}
