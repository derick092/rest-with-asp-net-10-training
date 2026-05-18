using Microsoft.AspNetCore.Mvc.Filters;

namespace RWAN10T.Api.Hypermdia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich (ResultExecutingContext context);
    }
}
