using Scalar.AspNetCore;

namespace RWAN10T.Api.Configurations
{
    public static class ScalarConfig
    {
        private static readonly string AppName = "RWAN10T API";
        private static readonly string AppDescription = "Domine ASP .NET 10 Swagger Docker Kubernetes REST Web API RESTful JWT xUnit Testcontainers React JS do 0 à Azure, GCP e+";

        public static WebApplication UseScalarConfiguration(this WebApplication app) 
        {
            app.MapScalarApiReference("/scalar", options => 
            {
                options.WithTitle(AppName);
                options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
            });

            return app;
        }
    }
}
