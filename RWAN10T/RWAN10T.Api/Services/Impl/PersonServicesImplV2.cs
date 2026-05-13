using RWAN10T.Api.Data.Converter.Contract;
using RWAN10T.Api.Data.Converter.Impl;
using RWAN10T.Api.Data.DTO.V2;
using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImplV2
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonServicesImplV2(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonDTO Create(PersonDTO person)
        {
            var obj = _converter.Parse(person)!;
            var createdObj = _repository.Create(obj);
            return ((IParser<Person, PersonDTO>)_converter).Parse(createdObj)!;
        }
    }
}
