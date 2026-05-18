using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace RWAN10T.Api.Hypermdia.Utils
{
    public static class UrlHelper
    {
        private static readonly object _lock = new();

        public static string BuildBaseUrl(this IUrlHelper helper, string routeName, string path) 
        {
            lock (_lock) 
            {
                var url = helper.Link(routeName, new { controller = path }) ?? string.Empty;
                if (url == null) return string.Empty;

                url.Replace("%2F", path).TrimEnd('/');
                return url;
            }
        }
    }
}
