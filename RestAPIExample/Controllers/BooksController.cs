using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPIExample.BL;
using System;
using System.Linq;
using System.Net.Mime;

namespace RestAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Return all books
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllBooks([FromQuery] int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Invalid count", nameof(count));
            }

           var books = Database.Books.Take(count);

            if (books.Any() == false)
                return NotFound();

            return Ok(books);
        }

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = Database.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Add book to list
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            Database.Books.Add(book);
            return Created("", book);
        }

        /// <summary>
        /// Update book data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookUpdate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete all books
        /// </summary>
        /// <returns></returns>
        [HttpDelete("all")]
        public IActionResult Delete()
        {
            Database.Books.RemoveAll(x => x.Id > 0);
            return Ok();
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
