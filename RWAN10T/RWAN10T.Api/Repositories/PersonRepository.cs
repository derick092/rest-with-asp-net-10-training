using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Repositories
{
    public class PersonRepository(MSSQLContext context) : GenericRepository<Person>(context), IPersonRepository
    {
        public Person Disable(long id)
        {
            var person = FindById(id);
            if(person != null)
            {
                person.Enable = false;
                Update(person);
            }
    
            return person!;
        }
    }
}
