using RWAN10T.Api.Model;

namespace RWAN10T.Api.Repositories
{
    public interface IBookRepository
    {
        Book Create(Book person);
        Book? FindById(long id);
        List<Book> FindAll();
        Book? Update(Book person);
        void Delete(long id);
    }
}
