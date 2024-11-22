using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Automapper
{
    public static class AutomapperExtension
    {
        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ManuscriptMappingProfile), 
                typeof(ReviewRequestMappingProfile)
            );
        }
    }
}
