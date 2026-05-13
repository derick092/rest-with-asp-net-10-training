using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IRepository<Person> _repository;
        public PersonServicesImpl(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }
        public Person? Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
           _repository.Delete(id);
        }
        public Person? FindById(long id)
        {
           return _repository.FindById(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
