using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Base;

namespace RWAN10T.Api.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T? FindById(long id);
        List<T> FindAll();
        T? Update(T item);
        void Delete(long id);
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
