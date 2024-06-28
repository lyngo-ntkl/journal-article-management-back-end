using System.Text;
using API.Configurations;
using API.CronJob;
using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Utils {
    public static class DependencyInjection {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration) {
            // dbcontext
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
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
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddSingleton<FirebaseConfiguration>();
            // swagger
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new OpenApiInfo() {
                    Title = "Journal Article API",
                    Version = "v1"
                });
            });
            // scheduler
            services.AddScoped<PermanentDeletionJob>();
            // cors
            services.AddCors(options => {
                options.AddPolicy("journal-article-management-policy", policy => {
                    // TODO: add new origin when deploy
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            // authentication & authorization
            services
                .AddAuthentication(config => {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config => {
                    config.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("security:secret-key")!)),
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetValue<string>("security:issuer"),
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization();
            // http context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}