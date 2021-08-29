using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPIExample.BL;
using System.Collections.Generic;
using System.Linq;

namespace RestAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
           var books = Database.Books;

            if (books.Any() == false)
                return NotFound();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = Database.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            Database.Books.Add(book);
            return Created("", book);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, JsonPatchDocument<Book> bookUpdate)
        {
            var book = Database.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            // Wiem, że to głupie: remove i insert
            // Ale w tym projekcie chodzi głównie o REST API
            Database.Books.Remove(book);

            bookUpdate.ApplyTo(book);

            Database.Books.Add(book);

            return NoContent();
        }

        [HttpDelete("all")]
        public IActionResult Delete()
        {
            Database.Books.RemoveAll(x => x.Id > 0);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = Database.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return BadRequest();

            Database.Books.Remove(book);
            return Ok();
        }
    }
}
