using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Repositories.Impl
{
    public class BookRepositoryImpl : IBookRepository
    {
        private MSSQLContext _context;
        public BookRepositoryImpl(MSSQLContext context)
        {
            _context = context;
        }

        public Book Create(Book person)
        {
            _context.Add(person);
            _context.SaveChanges();
            return person;
        }
        public Book? Update(Book person)
        {
            var existingPerson = _context.Books.Find(person.Id);
            if (existingPerson != null)
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.SaveChanges(); return person;
            }

            return null;
        }

        public void Delete(long id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
        public Book? FindById(long id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }
    }
}
