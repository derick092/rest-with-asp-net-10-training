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
        private static PersonDTO _person;

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

        [Fact(DisplayName = "01 - Create person")]
        [TestPriority(1)]
        public async Task CreatePerson_ShouldReturnCreatedPerson()
        {
            // Arrange
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
            _person = created;
        }

        [Fact(DisplayName = "03 - Disable person")]
        [TestPriority(3)]
        public async Task DisablePerson_ShouldReturnDisabledPerson()
        {
            // Arrange

            // Act
            var response = await _client.PatchAsync($"api/person/v1/{_person.Id}", null);


            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content.ReadFromJsonAsync<PersonDTO>();
            updated.Should().NotBeNull();
            updated.Enable.Should().BeFalse();
            _person = updated;
        }
    }
}
