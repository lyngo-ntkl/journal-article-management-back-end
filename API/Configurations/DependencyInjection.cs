using API.Configurations;
using API.CronJob;
using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;

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

            services.AddScoped<FileConverter, FileConverterImplementation>();
            // controllers
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });
            // mapper
            services.AddAutoMapper(typeof(MapperConfiguration));
            services.AddSingleton<FirebaseConfiguration>();
            // swagger
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new OpenApiInfo() {
                    Title = "Journal Article API",
                    Version = "v1"
                });
            });
            services.AddScoped<PermanentDeletionJob>();
        }
    }
}