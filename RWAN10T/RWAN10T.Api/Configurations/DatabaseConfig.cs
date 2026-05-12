using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration["MSSQLServerConnection:MSSQLServerConnectionString"];

            if (String.IsNullOrEmpty(connectionString)) 
                throw new ArgumentNullException("Connection string 'MSSQLServerConnectionString' not found!");

            services.AddDbContext<MSSQLContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
