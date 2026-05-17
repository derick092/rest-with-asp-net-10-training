using RWAN10T.Api.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace RWAN10T.Tests.IntegrationTests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get;  }
        public string ConnectionString => Container.GetConnectionString();

        public SqlServerFixture()
        {
            Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("@Admin123")
                .Build();

        }

        public async Task InitializeAsync()
        {
            await Container.StartAsync();

            // Aguarda até que o SQL Server dentro do container esteja aceitando conexões.
            var connStr = ConnectionString;
            const int maxAttempts = 30;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    using var conn = new SqlConnection(connStr);
                    await conn.OpenAsync();
                    conn.Close();
                    break;
                }
                catch
                {
                    if (attempt == maxAttempts) throw;
                    await Task.Delay(1000);
                }
            }

            EvolveConfig.ExecuteMigrations(connStr);
        }

        public async Task DisposeAsync()
        {
            await Container.DisposeAsync();
        }

    }
}
