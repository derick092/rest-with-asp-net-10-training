using RWAN10T.Api.Hypermdia.Enricher;
using RWAN10T.Api.Hypermdia.Filters;

namespace RWAN10T.Api.Configurations
{
    public static class HATEOASConfig
    {
        public static IServiceCollection AddHATEOASConfiguration(this IServiceCollection services)
        {
            var filterOptions = new HypermediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);
            services.AddScoped<HypermediaFilter>();
            return services;
        }

        public static void UseHATEOASRoutes(this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute("Default", "{controller=values}/v1/{id?}");
        }
    }
}
