using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace RWAN10T.Tests.IntegrationTests.CORS
{
    [TestCaseOrderer("RWAN10T.Tests.IntegrationTests.Tools.PriorityOrder", "RWAN10T.Tests")]
    public class PersonCorsIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _client;
        public PersonCorsIntegrationTests(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
            _client = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                });


        }

        private void AddOriginHeader(string origin)
        {
            _client.DefaultRequestHeaders.Remove("Origin");
            _client.DefaultRequestHeaders.Add("Origin", origin);
        }

        [Fact(DisplayName = "01 - Create person with allowed origin")]
        [TestPriority(1)]
        public async Task CreatePerson_WithAllowedOrigin_ShouldReturnSuccess()
        {
            // Arrange
            AddOriginHeader("http://localhost:3000");
            var request = new PersonDTO
            {
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua um, 1",

            };

            // Act
            var response = await _client.PostAsJsonAsync("api/person/v1", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<PersonDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "02 - Create person with unallowed origin")]
        [TestPriority(2)]
        public async Task CreatePerson_WithUnallowedOrigin_ShouldReturnForbidden()
        {
            // Arrange
            AddOriginHeader("http://unallowed-origin.com");
            var request = new PersonDTO
            {
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua um, 1",

            };

            // Act
            var response = await _client.PostAsJsonAsync("api/person/v1", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("Cors origin not allowed");
        }
    }
}
