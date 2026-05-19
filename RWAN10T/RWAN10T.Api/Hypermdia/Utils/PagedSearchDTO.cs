using RWAN10T.Api.Hypermdia.Abstract;

namespace RWAN10T.Api.Hypermdia.Utils
{
    public class PagedSearchDTO<T> where T : ISupportsHypermedia
     {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortDirection { get; set; } = "asc";
        public string SortFields { get; set; } = String.Empty;
        public Dictionary<string, object> Filters { get; set; } = [];
        public List<T> List { get; set; } = [];

        public PagedSearchDTO()
        {
            
        }

        public PagedSearchDTO(int currentPage, int pageSize, string sortFields, string sortDirection, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirection = sortDirection;
            Filters = filters ?? [];
        }

        public PagedSearchDTO(int currentPage, string sortFields, string sortDirection) : this(currentPage, 10, sortFields, sortDirection, null!) { }

        public int GetCurrentPage() =>
           CurrentPage == 0 ? 1 : CurrentPage;

        public int GetPageSize() =>
            PageSize == 0 ? 10 : PageSize;
    }
}
