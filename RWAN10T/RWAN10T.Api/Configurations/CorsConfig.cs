namespace RWAN10T.Api.Configurations
{
    public static class CorsConfig
    {
        private static string[] GetAllowedOrigins (IConfiguration configuration) =>
            configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
                    policy => policy.WithOrigins(GetAllowedOrigins(configuration))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }

        public static IApplicationBuilder UseCorsConfiguration(this WebApplication app, IConfiguration configuration)
        {
            //var origins = GetAllowedOrigins(configuration);
            ////force cors on test environment
            //app.Use(async (context, next) =>
            //{
            //    var origin= context.Request.Headers["Origin"].ToString();

            //    // If an origin is present and it's NOT in the allowed list, block it
            //    if(!string.IsNullOrEmpty(origin) && !origins.Contains(origin, StringComparer.OrdinalIgnoreCase))
            //    {
            //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //        await context.Response.WriteAsync("Cors origin not allowed");
            //    }
            
            //    await next();
            //});

            app.UseCors("DefaultPolicy");
            return app;
        }
    }
}
