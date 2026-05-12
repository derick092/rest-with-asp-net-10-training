using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Model;
using RWAN10T.Api.Services;

namespace RWAN10T.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonServices _personServices;

        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAll()
        {
            var persons = _personServices.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var person = _personServices.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            var createdPerson = _personServices.Create(person);
            return Ok(createdPerson);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            var updatedPerson = _personServices.Update(person);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personServices.Delete(id);
            return NoContent();
        }
    }
}
