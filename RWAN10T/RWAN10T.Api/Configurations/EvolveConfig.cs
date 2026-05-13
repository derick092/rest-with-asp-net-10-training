using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace RWAN10T.Api.Configurations
{
    public static class EvolveConfig
    {
        public static IServiceCollection AddEvolveConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            if (enviroment.IsDevelopment()) 
            {
                var evolveConnectionString = configuration["MSSQLServerConnection:MSSQLServerConnectionString"];

                if (String.IsNullOrEmpty(evolveConnectionString))
                    throw new ArgumentNullException("Connection string 'MSSQLServerConnectionString' not found!");

                try
                {
                    using var evolveConnection = new SqlConnection(evolveConnectionString);
                    var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
                    {
                        Locations = new[] { "db/migrations", "db/dataset" },
                        IsEraseDisabled = true,
                    };
                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while applying database migrations.");
                    throw;
                }
            }

            return services;
        }
    }
}
