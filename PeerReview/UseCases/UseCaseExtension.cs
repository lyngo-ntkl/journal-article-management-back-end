using Microsoft.Extensions.DependencyInjection;
using UseCases.Manuscripts.Create;
using UseCases.Manuscripts.Get;

namespace UseCases
{
    public static class UseCaseExtension
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddScoped<ISubmitManuscript, SubmitManuscriptHandler>();
            services.AddScoped<IGetManuscripts, GetManuscriptsHandler>();
            return services;
        }
    }
}