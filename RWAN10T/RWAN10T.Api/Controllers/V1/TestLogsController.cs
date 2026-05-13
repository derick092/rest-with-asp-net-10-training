using Microsoft.AspNetCore.Mvc;

namespace RWAN10T.Api.Controllers.V1
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class TestLogsController : Controller
    {
        private readonly ILogger<TestLogsController> _logger;

        public TestLogsController(ILogger<TestLogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult LogTest()
        {
            _logger.LogInformation("This is an informational log message.");
            _logger.LogWarning("This is a warning log message.");
            _logger.LogError("This is an error log message.");
            _logger.LogDebug("This is a debug log message.");
            _logger.LogTrace("This is a trace log message.");
            _logger.LogCritical("This is a critical log message.");
            return Ok("Logs have been generated. Check the console output.");
        }
    }
}
