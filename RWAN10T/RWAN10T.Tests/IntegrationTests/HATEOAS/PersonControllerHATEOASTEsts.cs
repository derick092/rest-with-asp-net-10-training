using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;
using RWAN10T.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace RWAN10T.Tests.IntegrationTests.HATEOAS
{
    [TestCaseOrderer("RWAN10T.Tests.IntegrationTests.Tools.PriorityOrder", "RWAN10T.Tests")]
    public class PersonControllerHATEOASTEsts : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static PersonDTO? _person;

        public PersonControllerHATEOASTEsts(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(
                sqlServerFixture.ConnectionString);

            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                }
            );
        }

        private void AssertLinkPattern(string content, string rel) 
        {
            var pattern =
              $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/person/v1.*?""";
            Regex.IsMatch(content, pattern).Should()
                .BeTrue($"Link with rel='{rel}' should exist and have valid href");

        }

        [Fact(DisplayName = "01 - Create Person")]
        [TestPriority(1)]
        public async Task CreatePerson_ShouldContainHateoasLinks()
        {
            var request = new PersonDTO
            {
                FirstName = "Mm",
                LastName = "Berto",
                Address = "Um, 1",
                Gender = "Male",
                Enable = true
            };

            var response = await _httpClient.PostAsJsonAsync(
                "/api/person/v1", request);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            _person = await response.Content.ReadFromJsonAsync<PersonDTO>();

            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");
        }

        [Fact(DisplayName = "03 - Disable Person By Id")]
        [TestPriority(3)]
        public async Task DisablePersonById_ShouldContainHateoasLinks()
        {
            var response = await _httpClient.PatchAsync(
                $"/api/person/v1/{_person!.Id}", null);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            _person = await response.Content.ReadFromJsonAsync<PersonDTO>();

            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");

        }

        [Fact(DisplayName = "04 - Get Person By Id")]
        [TestPriority(4)]
        public async Task GetPersonById_ShouldContainHateoasLinks()
        {
            var response = await _httpClient.GetAsync(
                $"/api/person/v1/{_person!.Id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            _person = await response.Content.ReadFromJsonAsync<PersonDTO>();

            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");
        }

    }
}
