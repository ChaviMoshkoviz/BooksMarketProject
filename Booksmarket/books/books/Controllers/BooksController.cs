
using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Authorization;
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
        // בונוס: רשימת ספרים הממתינים לאישור (רק למנהל)
        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingBooks()
        {
            // קריאה לפונקציה החדשה בסרביס
            var pending = await _service.GetPendingBooks();

            if (pending == null || !pending.Any())
                return Ok(new { Message = "No books are currently waiting for approval." });

            // מיפוי ל-DTO והחזרה למנהל
            var booksDto = _mapper.Map<IEnumerable<BooksDTO>>(pending);
            return Ok(booksDto);
        }
        [Authorize(Roles = "Admin")] // רק מנהל יכול לאשר ספרים
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveBook(int id)
        {
            var approvedBook = await _service.ApproveBook(id);
            if (approvedBook == null)
                return NotFound("Book not found");

            return Ok(new { Message = "Book approved successfully", Book = _mapper.Map<BooksDTO>(approvedBook) });
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
