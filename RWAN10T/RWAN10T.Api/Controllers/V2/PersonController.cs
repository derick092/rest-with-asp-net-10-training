using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V2;
using RWAN10T.Api.Model;
using RWAN10T.Api.Services;
using RWAN10T.Api.Services.Impl;

namespace RWAN10T.Api.Controllers.V2
{
    [Route("api/[controller]/v2")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private PersonServicesImplV2 _personServices;
        private readonly ILogger<PersonController> _logger;

        public PersonController(PersonServicesImplV2 personServices, ILogger<PersonController> logger)
        {
            _personServices = personServices;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Creating new person: {firstName}", person.FirstName);
            if (person == null)
            {
                _logger.LogWarning("Failed to create person: {firstName}", person?.FirstName);
                return BadRequest(); 
            }
            var createdPerson = _personServices.Create(person);
            return Ok(createdPerson);
        }
    }
}
