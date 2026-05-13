using RWAN10T.Api.Data.DTO;
using RWAN10T.Api.Model;

namespace RWAN10T.Api.Services
{
    public interface IPersonServices
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO? FindById(long id);
        List<PersonDTO>? FindAll();
        PersonDTO? Update(PersonDTO person);
        void Delete(long id);
    }
}
