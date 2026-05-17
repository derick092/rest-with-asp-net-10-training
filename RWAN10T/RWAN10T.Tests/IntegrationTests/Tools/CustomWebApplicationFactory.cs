using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWAN10T.Tests.IntegrationTests.Tools
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
            // Ensure the flag is visible to Program startup even if ConfigureAppConfiguration runs later
            Environment.SetEnvironmentVariable("RUN_MIGRATIONS", "false");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var inMemorySettings = new Dictionary<string, string>
                {
                    ["MSSQLServerConnection:MSSQLServerConnectionString"] = _connectionString,
                    // Disable migrations during test host startup; migrations are applied by the test fixture
                    ["RUN_MIGRATIONS"] = "false"
                };
                config.AddInMemoryCollection(inMemorySettings!);
            });
        }
    }
}
