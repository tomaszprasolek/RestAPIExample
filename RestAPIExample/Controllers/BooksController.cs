using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPIExample.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly Database db;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
            db = new Database();
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return db.Books;
        }

        [HttpGet("book/{id}")]
        public IActionResult Get(int id)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
