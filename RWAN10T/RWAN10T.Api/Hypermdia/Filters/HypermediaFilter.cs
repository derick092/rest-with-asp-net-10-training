using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RWAN10T.Api.Hypermdia.Filters
{
    public class HypermediaFilter(HypermediaFilterOptions options) : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _options = options;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult) 
            {
                var enricher = _options.ContentResponseEnricherList.FirstOrDefault(option => option.CanEnrich(context));
                enricher?.Enrich(context).Wait();
            }
        }
    }
}
