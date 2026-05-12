using RWAN10T.Api.Model;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        public Person Create(Person person)
        {
            person.Id = new Random().Next(1, 1000);
            return person;
        }
        public Person Update(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }
        public Person FindById(long id)
        {
            return MockPerson();
        }

        public List<Person> FindAll()
        {
            return new List<Person>()
            {
                MockPerson(),
                MockPerson(),
                MockPerson(),
                MockPerson(),
                MockPerson()
            };
        }

        private Person MockPerson() 
        {
            return new Person()
            {
                Id = new Random().Next(1, 1000),
                FirstName = "Rwan",
                LastName = "T",
                Address = "Rwanda",
                Gender = "Male"
            };
        }
    }
}
