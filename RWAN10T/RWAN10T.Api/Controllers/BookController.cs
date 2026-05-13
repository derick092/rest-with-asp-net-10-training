using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO;
using RWAN10T.Api.Model;
using RWAN10T.Api.Services;

namespace RWAN10T.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private IBookService _bookServices;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookServices, ILogger<BookController> logger)
        {
            _bookServices = bookServices;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<BookDTO>> GetAll()
        {
            _logger.LogInformation("Fetching all books");
            var books = _bookServices.FindAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            _logger.LogInformation("Fetching book with ID {Id}", id);
            var book = _bookServices.FindById(id);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookDTO book)
        {
            _logger.LogInformation("Creating new book: {title}", book.Title);
            if (book == null)
            {
                _logger.LogWarning("Failed to create book: {title}", book?.Title);
                return BadRequest();
            }
            var createdBook = _bookServices.Create(book);
            return Ok(createdBook);
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookDTO book)
        {
            _logger.LogInformation("Updating book with {id}", book.Id);
            if (book == null)
            {
                _logger.LogError("Failed to update book: {id}", book?.Id);
                return BadRequest();
            }
            var updatedBook = _bookServices.Update(book);

            _logger.LogDebug("Book with ID {id} updated successfully", book.Id);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting book with {id}", id);
            _bookServices.Delete(id);

            _logger.LogDebug("Book with ID {id} deleted successfully", id);
            return NoContent();
        }
    }
}
