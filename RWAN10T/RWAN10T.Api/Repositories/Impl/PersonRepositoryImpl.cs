using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Repositories.Impl
{
    public class PersonRepositoryImpl : IPersonRepository
    {
        private MSSQLContext _context;
        public PersonRepositoryImpl(MSSQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
            return person;
        }
        public Person? Update(Person person)
        {
            var existingPerson = _context.Persons.Find(person.Id);
            if (existingPerson != null)
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.SaveChanges(); return person;
            }

            return null;
        }

        public void Delete(long id)
        {
            var person = _context.Persons.Find(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
        public Person? FindById(long id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }
    }
}
