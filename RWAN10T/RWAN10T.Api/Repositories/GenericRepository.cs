using Microsoft.EntityFrameworkCore;
using RWAN10T.Api.Model.Base;
using RWAN10T.Api.Model.Context;

namespace RWAN10T.Api.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MSSQLContext _context;
        private DbSet<T> _dataSet;

        public GenericRepository(MSSQLContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public T Create(T item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(long id)
        {
            var item = _dataSet.Find(id);
            if (item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
        }

        public bool Exists(long id)
        {
            return _dataSet.Any(e => e.Id == id);
        }

        public List<T> FindAll()
        {
            return _dataSet.ToList();
        }

        public T? FindById(long id)
        {
            return _dataSet.Find(id);
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return [.. _dataSet.FromSqlRaw(query)];
        }

        public int GetCount(string query)
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public T? Update(T item)
        {
            var existingItem = _context.Books.Find(item.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                _context.SaveChanges(); return item;
            }

            return null;
        }
    }
}
