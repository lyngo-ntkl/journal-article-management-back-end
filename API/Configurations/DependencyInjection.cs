using API.Configurations;
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
            services.AddScoped<ArticleRepository, ArticleRepositoryImplementation>();
            services.AddScoped<TopicRepository, TopicRepositoryImplementation>();
            services.AddScoped<ReferenceRepository, ReferenceRepositoryImplementation>();
            // services
            services.AddScoped<UserService, UserServiceImplementation>();
            services.AddScoped<ArticleService, ArticleServiceImplementation>();
            services.AddScoped<FirebaseStorageService, FirebaseStorageServiceImplementation>();
            // controllers
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });
            // mapper
            services.AddAutoMapper(typeof(MapperConfiguration));
            services.AddSingleton<FirebaseConfiguration>();
        }
    }
}