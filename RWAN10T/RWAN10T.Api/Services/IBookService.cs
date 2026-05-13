using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;

namespace RWAN10T.Api.Services
{
    public interface IBookService
    {
        BookDTO Create(BookDTO person);
        BookDTO? FindById(long id);
        List<BookDTO>? FindAll();
        BookDTO? Update(BookDTO person);
        void Delete(long id);
    }
}
