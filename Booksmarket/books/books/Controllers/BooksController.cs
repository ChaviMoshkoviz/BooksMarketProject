using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
   private IDataContext _context;
        public BooksController(IDataContext context)
        { 
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            if (!(_context.books).Any())
                return NotFound("No books found");
            return Ok(_context.books);
        }
        [HttpGet("author/{Author}")]
        public IActionResult GetBooksByAuthor(string authorName)
        {
            var filteredBooks = _context.books.FindAll(b => b.Author.ToLower() == authorName.ToLower());
            if (filteredBooks.Count == 0)
                return NotFound($"No books found by author {authorName}");
            return Ok(filteredBooks);
        }

        [HttpGet("genre/{Genre}")]
        public IActionResult GetBooksByGerds(string gerdsBook)
        {
            var filteredBooks = _context.books.FindAll(B => B.Genre.ToLower() == gerdsBook.ToLower());
            if (filteredBooks.Count == 0)
                return NotFound($"No books found from category {gerdsBook}");
            return Ok(filteredBooks);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Books newBook)
        {

            newBook.BookId = _context.books.Count + 1;
            _context.books.Add(newBook);
            return Ok(newBook);
        }
    }
}
