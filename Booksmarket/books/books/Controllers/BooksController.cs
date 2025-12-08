
using AutoMapper;
using Books.core.DTO;
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
        private readonly IMapper _mapper;
        public BooksController(IBooksService service ,IMapper mapper)
        { 
            _service=service;
            _mapper=mapper;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
          var books = _service.GetAllBooks();
            if(!books.Any())
                return NotFound("no books found");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(books);
            return Ok(booksDto);
        
        }
        [HttpGet("author/{Author}")]
        public IActionResult GetBooksByAuthor(string Author)
        {
            var filteredBooks=_service.GetBooksByAuthor(Author);
            if (!filteredBooks.Any())
                return NotFound($"No books found by author {Author}");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(filteredBooks);
            return Ok(booksDto);
        }

        [HttpGet("genre/{Genre}")]
        public IActionResult GetBooksByGenre(string Genre)
        {
            var filteredBooks = _service.GetBooksByGenre(Genre);
            if (!filteredBooks.Any())
                return NotFound($"No books found by category {Genre}");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(filteredBooks);
            return Ok(booksDto);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newbook)
        {
          
            return Ok(_service.AddBook(newbook));
        }

    }
}
