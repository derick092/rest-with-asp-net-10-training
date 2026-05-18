using RWAN10T.Api.Hypermdia.Abstract;

namespace RWAN10T.Api.Hypermdia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = [];
    }
}
