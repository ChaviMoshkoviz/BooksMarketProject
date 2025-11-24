
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;
        public BooksController(IBooksService service)
        { 
            _service=service;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
     var books = _service.GetAllBooks();
            if(!books.Any())
                return NotFound("no books found");
            return Ok(books);
        
        }
        [HttpGet("author/{Author}")]
        public IActionResult GetBooksByAuthor(string Author)
        {
            var filteredBooks=_service.GetBooksByAuthor(Author);
            if (!filteredBooks.Any())
                return NotFound($"No books found by author {Author}");
            return Ok(filteredBooks);
        }

        [HttpGet("genre/{Genre}")]
        public IActionResult GetBooksByGenre(string Genre)
        {
            var filteredBooks = _service.GetBooksByGenre(Genre);
            if (!filteredBooks.Any())
                return NotFound($"No books found by category {Genre}");
            return Ok(filteredBooks);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newbook)
        {
            return Ok(_service.AddBook(newbook));
        }

    }
}
