using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Infrastructure.gRPC
{
    public static class GrpcExtension
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddGrpcSwagger();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Peer review",
                    Version = "v1"
                });
                //var filePath = Path.Combine(AppContext.BaseDirectory, "MyApi.xml");
                //config.IncludeXmlComments(filePath);
            });
        }

        public static void MapSwaggerConfiguration(this WebApplication app)
        {
            app.UseSwagger();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Peer review API v1");
                });
            }

            //app.MapGet("", () => { })
            //    .WithDescription("");
        }
    }
}
