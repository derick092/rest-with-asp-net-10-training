using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Model;
using RWAN10T.Api.Services;

namespace RWAN10T.Api.Controllers.V1
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonServices _personServices;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonServices personServices, ILogger<PersonController> logger)
        {
            _personServices = personServices;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult<List<Person>> GetAll()
        {
            _logger.LogInformation("Fetching all persons");
            var persons = _personServices.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetById(long id)
        {
            _logger.LogInformation("Fetching person with ID {Id}", id);
            var person = _personServices.FindById(id);
            if (person == null)
            { 
                _logger.LogWarning("Person with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(person);
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
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
            Response.Headers.Add("X-API-Deprecated", "true");
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
            Response.Headers.Add("X-API-Deprecation-Date", "2026-12-31");
#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
            return Ok(createdPerson);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Updating person with {id}", person.Id);
            if (person == null) 
            {
                _logger.LogError("Failed to update person: {id}", person?.Id);
                return BadRequest(); 
            }
            var updatedPerson = _personServices.Update(person);

            _logger.LogDebug("Person with ID {id} updated successfully", person.Id);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting person with {id}", id);
            _personServices.Delete(id);

            _logger.LogDebug("Person with ID {id} deleted successfully", id);
            return NoContent();
        }
    }
}
