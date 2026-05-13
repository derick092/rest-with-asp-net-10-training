using Mapster;
using RWAN10T.Api.Data.DTO;
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

        public BookDTO Create(BookDTO book)
        {
            var entity = book.Adapt<Book>();
            return _repository.Create(entity).Adapt<BookDTO>();
        }
        public BookDTO? Update(BookDTO book)
        {
            var entity = book.Adapt<Book>();
            return _repository.Update(entity)?.Adapt<BookDTO>();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        public BookDTO? FindById(long id)
        {
            return _repository.FindById(id).Adapt<BookDTO>();
        }

        public List<BookDTO>? FindAll()
        {
            return _repository.FindAll().Adapt<List<BookDTO>>();
        }
    }
}
