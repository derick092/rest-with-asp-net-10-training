using RWAN10T.Api.Data.Converter.Impl;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonServicesImpl(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonDTO Create(PersonDTO person)
        {
            return _converter.Parse(_repository.Create(_converter.Parse(person)!))!;
        }
        public PersonDTO? Update(PersonDTO person)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(person)!)!);
        }

        public void Delete(long id)
        {
           _repository.Delete(id);
        }
        public PersonDTO? FindById(long id)
        {
           return _converter.Parse(_repository.FindById(id)!);
        }

        public List<PersonDTO>? FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }
    }
}
