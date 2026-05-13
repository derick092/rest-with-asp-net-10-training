using RWAN10T.Api.Model;
using RWAN10T.Api.Repositories;

namespace RWAN10T.Api.Services.Impl
{
    public class BookServicesImpl : IBookService
    {
        private IRepository<Book> _repository;
        public BookServicesImpl(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }
        public Book? Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        public Book? FindById(long id)
        {
            return _repository.FindById(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
