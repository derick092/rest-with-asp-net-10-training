using RWAN10T.Api.Hypermdia.Abstract;

namespace RWAN10T.Api.Model
{
    public class PagedSearch<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortDirection { get; set; } = "asc";
        public List<T> List { get; set; } = [];
    }
}
