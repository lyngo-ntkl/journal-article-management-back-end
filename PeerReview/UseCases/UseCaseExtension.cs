using Microsoft.Extensions.DependencyInjection;

namespace UseCases
{
    public static class UseCaseExtension
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            return services;
        }
    }
}