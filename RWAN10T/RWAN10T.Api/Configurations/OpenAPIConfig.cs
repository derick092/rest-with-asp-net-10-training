using Microsoft.OpenApi;

namespace RWAN10T.Api.Configurations
{
    public static class OpenAPIConfig
    {
        private static readonly string AppName = "RWAN10T API";
        private static readonly string AppDescription = "Domine ASP .NET 10 Swagger Docker Kubernetes REST Web API RESTful JWT xUnit Testcontainers React JS do 0 à Azure, GCP e+";

        public static IServiceCollection AddOpenAPIConfig(this IServiceCollection services)
        {
            services.AddSingleton(new OpenApiInfo
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

            return services;
        } 
    }
}
