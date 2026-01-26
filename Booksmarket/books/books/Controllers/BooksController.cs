
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
        public async Task < IActionResult> GetAllBooks()
        {
          var books =await _service.GetAllBooks();
            if(!books.Any())
                return NotFound("no books found");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(books);
            return Ok(booksDto);
        
        }
        [HttpGet("author/{Author}")]
        public async Task<IActionResult> GetBooksByAuthor(string Author)
        {
            var filteredBooks = await _service.GetBooksByAuthor(Author);
            if (!filteredBooks.Any() ||filteredBooks==null)
                return NotFound($"No books found by author {Author}");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(filteredBooks);
            return Ok(booksDto);
        }

        [HttpGet("genre/{Genre}")]
        public async Task < IActionResult> GetBooksByGenre(string Genre)
        {
            var filteredBooks = await _service.GetBooksByGenre(Genre);
            if (!filteredBooks.Any() || filteredBooks==null)
                return NotFound($"No books found by category {Genre}");
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(filteredBooks);
            return Ok(booksDto);
        }
        [HttpPost]
        public async Task< IActionResult> AddBook([FromBody] BooksDTO newBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // 1. מיפוי מה-DTO שקיבלנו מהמשתמש לישות של בסיס הנתונים
            var bookEntity = _mapper.Map<Book>(newBookDto);

            // 2. שמירה בבסיס הנתונים דרך הסרביס
            var addedBook = await _service.AddBook(bookEntity);

            // 3. החזרת התוצאה כ-DTO (אופציונלי אך מומלץ)
            return Ok(_mapper.Map<BooksDTO>(addedBook));
        }

    }
}
