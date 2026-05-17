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
                // Allow tests or other hosts to disable migrations via configuration
                var runMigrations = configuration["RUN_MIGRATIONS"];
                if (!string.IsNullOrEmpty(runMigrations) && string.Equals(runMigrations, "false", StringComparison.OrdinalIgnoreCase))
                {
                    Log.Information("Database migrations disabled by configuration (RUN_MIGRATIONS=false).");
                    return services;
                }

                var evolveConnectionString = configuration["MSSQLServerConnection:MSSQLServerConnectionString"];

                if (String.IsNullOrEmpty(evolveConnectionString))
                    throw new ArgumentNullException("Connection string 'MSSQLServerConnectionString' not found!");

                try
                {
                    ExecuteMigrations(evolveConnectionString);
                    
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while applying database migrations.");
                    throw;
                }
            }

            return services;
        }

        public static void ExecuteMigrations(string connectionString) 
        {
            using var evolveConnection = new SqlConnection(connectionString);

            const int maxRetries = 5;
            var delay = TimeSpan.FromSeconds(2);

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    evolveConnection.Open();
                    break;
                }
                catch (SqlException ex) when (attempt < maxRetries)
                {
                    Log.Warning(ex, "Tentativa {Attempt} falhou ao conectar ao banco. Retentando em {Delay}...", attempt, delay);
                    Thread.Sleep(delay);
                    delay = TimeSpan.FromSeconds(delay.TotalSeconds * 2);
                    continue;
                }
            }

            var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
            {
                Locations = new[] { "db/migrations", "db/dataset" },
                IsEraseDisabled = true,
            };
            evolve.Migrate();
        }
    }
}
