using Scalar.AspNetCore;

namespace RWAN10T.Api.Configurations
{
    public static class ScalarConfig
    {
        private static readonly string AppName = "RWAN10T API";
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
