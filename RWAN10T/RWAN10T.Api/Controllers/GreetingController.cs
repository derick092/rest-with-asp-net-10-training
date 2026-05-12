using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Model;
using System.Globalization;

namespace RWAN10T.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {

        private static long _counter = 0;
        private static readonly string _template = "Hello {0}!";

        [HttpGet]
        public Greeting Get([FromQuery] string name = "World")
        {
            var id = Interlocked.Increment(ref _counter);
            var content = String.Format(_template, name);
            return new Greeting(id, content);
        }
    }
}
