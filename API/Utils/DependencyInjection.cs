using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Utils {
    public static class DependencyInjection {
        public static void AddDependencies(this IServiceCollection services, string? connectionString) {
            // dbcontext
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString: connectionString));
            // repositories
            services.AddScoped<UnitOfWork, UnitOfWorkImplementation>();
            services.AddScoped<UserRepository, UserRepositoryImplementation>();
            // services
            services.AddScoped<UserService, UserServiceImplementation>();
            // controllers
            services.AddControllers();
        }
    }
}