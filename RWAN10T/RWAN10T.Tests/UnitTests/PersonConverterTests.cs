using FluentAssertions;
using RWAN10T.Api.Data.Converter.Contract;
using RWAN10T.Api.Data.Converter.Impl;
using RWAN10T.Api.Data.DTO.V2;
using RWAN10T.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWAN10T.Tests.UnitTests
{
    public class PersonConverterTests
    {
        private readonly PersonConverter _converter;

        public PersonConverterTests()
        {
            _converter = new PersonConverter();
        }

        //PersonDTO to Person conversion test
        [Fact]
        public void ParseShouldConvertPersonDTOToPerson()
        {
            //Arrange: prepare the data, objects, and dependencies required for the test
            var dto = new PersonDTO
            {
                Id = 1,
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua 1, 1",
                Gender = "Male",
                BirthDay = DateTime.Now
            };

            var expected = new Person()
            {
                Id = 1,
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua 1, 1",
                Gender = "Male",
            };

            //Act: execute the method or functionality under test
            var result = _converter.Parse(dto);

            //Assert: verify that the result matches the expected outcome
            result.Should().NotBeNull();
            result.Id.Should().Be(expected.Id);
            result.FirstName.Should().Be(expected.FirstName);
            result.LastName.Should().Be(expected.LastName);
            result.Address.Should().Be(expected.Address);
            result.Gender.Should().Be(expected.Gender);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseShouldReturnNullWhenDtoIsNull()
        {
            //Arrange: prepare the data, objects, and dependencies required for the test
            PersonDTO? dto = null;

            //Act: execute the method or functionality under test
#pragma warning disable CS8604 // Possível argumento de referência nula.
            Person? result = _converter.Parse(dto);
#pragma warning restore CS8604 // Possível argumento de referência nula.

            //Assert: verify that the result matches the expected outcome
            result.Should().BeNull();
        }

        [Fact]
        public void ParseShouldConvertPersonToPersonDTO()
        {
            //Arrange: prepare the data, objects, and dependencies required for the test
            var obj = new Person
            {
                Id = 1,
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua 1, 1",
                Gender = "Male"
            };

            var expected = new PersonDTO()
            {
                Id = 1,
                FirstName = "Um",
                LastName = "Berto",
                Address = "Rua 1, 1",
                Gender = "Male",
                BirthDay = DateTime.Now
            };

            //Act: execute the method or functionality under test
            var result = _converter.Parse(obj);

            //Assert: verify that the result matches the expected outcome
            result.Should().NotBeNull();
            result.Id.Should().Be(expected.Id);
            result.FirstName.Should().Be(expected.FirstName);
            result.LastName.Should().Be(expected.LastName);
            result.Address.Should().Be(expected.Address);
            result.Gender.Should().Be(expected.Gender);
            result.Should().BeEquivalentTo(expected, options => options.Excluding(o => o.BirthDay));
        }

        [Fact]
        public void ParseShouldReturnNullWhenPersonIsNull()
        {
            //Arrange: prepare the data, objects, and dependencies required for the test
            Person? obj = null;

            //Act: execute the method or functionality under test
#pragma warning disable CS8604 // Possível argumento de referência nula.
            PersonDTO? result = ((IParser<Person, PersonDTO>)_converter).Parse(obj);
#pragma warning restore CS8604 // Possível argumento de referência nula.

            //Assert: verify that the result matches the expected outcome
            result.Should().BeNull();
        }

        [Fact]
        public void ParseShouldConvertPersonDTOListToPersonList() 
        {
            //Arrange
            var obj = new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = 1,
                    FirstName = "Um",
                    LastName = "Berto",
                    Address = "Rua 1, 1",
                    Gender = "Male",
                    BirthDay = DateTime.MinValue
                },
                new PersonDTO
                {
                    Id = 1,
                    FirstName = "Dois",
                    LastName = "Berta",
                    Address = "Rua 2, 2",
                    Gender = "Female",
                    BirthDay = DateTime.MaxValue
                },
            };

            var expected = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "Um",
                    LastName = "Berto",
                    Address = "Rua 1, 1",
                    Gender = "Male",
                },
                new Person
                {
                    Id = 1,
                    FirstName = "Dois",
                    LastName = "Berta",
                    Address = "Rua 2, 2",
                    Gender = "Female",
                }
            };

            //Act
            var result = _converter.ParseList(obj);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expected.Count);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseShouldConvertPersonListToPersonDTOList()
        {
            //Arrange
            var obj = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "Um",
                    LastName = "Berto",
                    Address = "Rua 1, 1",
                    Gender = "Male",
                },
                new Person
                {
                    Id = 1,
                    FirstName = "Dois",
                    LastName = "Berta",
                    Address = "Rua 2, 2",
                    Gender = "Female",
                },
            };

            var expected = new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = 1,
                    FirstName = "Um",
                    LastName = "Berto",
                    Address = "Rua 1, 1",
                    Gender = "Male",
                    BirthDay = DateTime.MinValue
                },
                new PersonDTO
                {
                    Id = 1,
                    FirstName = "Dois",
                    LastName = "Berta",
                    Address = "Rua 2, 2",
                    Gender = "Female",
                    BirthDay = DateTime.MaxValue
                }
            };

            //Act
            var result = _converter.ParseList(obj);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expected.Count);
            result.Should().BeEquivalentTo(expected, options => options.Excluding(o => o.BirthDay));
        }
    }
}
