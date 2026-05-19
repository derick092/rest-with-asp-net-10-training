using Mapster;
using RWAN10T.Api.Hypermdia.Utils;
using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories.QueryBuilders;

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

        public List<Person> FindByName(string firstName, string lastName)
        {
            var query = _context.Persons.AsQueryable();

            if(!String.IsNullOrWhiteSpace(firstName))
                query = query.Where(p => p.FirstName.Contains(firstName));
            if(!String.IsNullOrWhiteSpace(lastName))
                query = query.Where(p => p.LastName.Contains(lastName));

            return [.. query];
        }

        public PagedSearch<Person> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new PersonQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(name, sortDirection, pageSize, page);

            var persons = base.FindWithPagedSearch(query);
            var totalResults = base.GetCount(countQuery);

            return new PagedSearch<Person>
            {
                CurrentPage = page,
                List = persons,
                PageSize = size,
                SortDirection = sort,
                TotalResults = totalResults
            };
        }
    }
}
