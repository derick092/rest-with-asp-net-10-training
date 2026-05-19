using Mapster;
using RWAN10T.Api.Data.Converter.Impl;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Hypermdia.Utils;
using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonServicesImpl(IPersonRepository repository)
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

        public PersonDTO? Disable(long id)
        {
            if (_repository != null)
            {
                var person = _repository.Disable(id);
                return _converter.Parse(person);
            }
            return null;
        }

        public List<PersonDTO> FindByName(string firstName, string lastName)
        {
            return _repository.FindByName(firstName, lastName).Adapt<List<PersonDTO>>();
        }

        public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);

            return new PagedSearchDTO<PersonDTO> 
            {
                CurrentPage = page,
                List = result.List.Adapt<List<PersonDTO>>(),
                PageSize = result.PageSize,
                SortDirection = result.SortDirection,
                TotalResults = result.TotalResults
            };
        }
    }
}
