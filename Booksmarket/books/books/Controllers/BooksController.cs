
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
        private readonly BooksService _service;
        public BooksController(BooksService service)
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
        public IActionResult GetBooksByAuthor(string author)
        {
            var filteredBooks=_service.GetBooksByAuthor(author);
            if (!filteredBooks.Any())
                return NotFound($"No books found by author {author}");
            return Ok(filteredBooks);
        }

        [HttpGet("genre/{Genre}")]
        public IActionResult GetBooksByGenre(string genre)
        {
            var filteredBooks = _service.GetBooksByGenre(genre);
            if (!filteredBooks.Any())
                return NotFound($"No books found by category {genre}");
            return Ok(filteredBooks);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newbook)
        {
            return Ok(_service.AddBook(newbook));
        }
    }
}
