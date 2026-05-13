using Microsoft.OpenApi;

namespace RWAN10T.Api.Configurations
{
    public static class SwaggerConfig
    {
        private static readonly string AppName = "RWAN10T API";
        private static readonly string AppDescription = "Domine ASP .NET 10 Swagger Docker Kubernetes REST Web API RESTful JWT xUnit Testcontainers React JS do 0 à Azure, GCP e+";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "RWAN10T",
                        Email = "RWAN10T@example.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.CustomSchemaIds(type => type.FullName);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RWAN10T API v1");
                options.RoutePrefix = "swagger-ui";
                options.DocumentTitle = AppName;
            });

            return app;
        }
    }
}
