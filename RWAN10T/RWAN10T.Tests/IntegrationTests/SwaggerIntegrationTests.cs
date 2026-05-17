using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RWAN10T.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWAN10T.Tests.IntegrationTests
{
    public class SwaggerIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _client;

        public SwaggerIntegrationTests(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
            _client = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                });


        }

        [Fact]
        public async Task SwaggerJson_ShouldReturnSwaggerJson() 
        {
            //Arrenge n Act
            var response = await _client.GetAsync("/swagger/v1/swagger.json");

            //Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain("/api/person/v1");
        }

        [Fact]
        public async Task SwaggerUI_ShouldReturnSwaggerUI() 
        {
            //Arrenge n Act
            var response = await _client.GetAsync("/swagger-ui/index.html");
            //Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain("<div id=\"swagger-ui\">");
        }
    }
}
