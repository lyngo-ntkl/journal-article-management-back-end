using API.Entities;
using API.Repositories;

namespace API.Utils {
    public static class DependencyInjection {
        public static void AddDependencies(this IServiceCollection services) {
            // dbcontext
            services.AddDbContext<ApplicationDbContext>();
            // repositories
            services.AddScoped<UnitOfWork, UnitOfWorkImplementation>();
            services.AddScoped<UserRepository, UserRepositoryImplementation>();
            // services
            // controllers
            services.AddControllers();
        }
    }
}