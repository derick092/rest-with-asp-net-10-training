using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RWAN10T.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWAN10T.Tests.IntegrationTests
{
    public class ScalarIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _client;

        public ScalarIntegrationTests(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
            _client = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                });


        }

        [Fact]
        public async Task ScalarUI_ShouldReturnSwaggerUI()
        {
            //Arrenge n Act
            var response = await _client.GetAsync("/scalar/");
            //Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("script src");
        }
    }
}
