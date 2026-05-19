using FluentAssertions;
using RWAN10T.Api.Repositories.QueryBuilders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWAN10T.Tests.UnitTests
{
    public class PersonQueryBuilderTests
    {
        private readonly PersonQueryBuilder _queryBuilder;
        public PersonQueryBuilderTests()
        {
            _queryBuilder = new PersonQueryBuilder();
        }

        [Fact]
        public void BuildQueries_ShouldReturnCorrectQueries()
        {
            // Arrange
            var name = "John";
            var sortDirection = "asc";
            var pageSize = 10;
            var page = 2;

            // Act
            var (query, countQuery, sort, size, offset) = _queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            // Assert
            query.Should().Contain("SELECT * FROM Person p WHERE 1=1  AND (p.first_name LIKE '%John%')");
            query.Should().Contain("ORDER BY p.first_name asc");
            query.Should().Contain("OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY");

            countQuery.Should().Be("SELECT COUNT(*) FROM Person p WHERE 1=1  AND (p.first_name LIKE '%John%')  ");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(10);
        }
    }
}
