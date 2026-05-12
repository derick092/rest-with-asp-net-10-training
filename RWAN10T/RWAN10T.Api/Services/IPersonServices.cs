using RWAN10T.Api.Model;

namespace RWAN10T.Api.Services
{
    public interface IPersonServices
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
