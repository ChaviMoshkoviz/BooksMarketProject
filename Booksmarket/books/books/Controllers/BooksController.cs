using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public static List<Books> books = new List<Books>
        {
            /*tension=מתח*/
            new Books {BookId = 1,Title="Duplicatim 1",Author="Yonah sapir",Genre="fantasy thriller",Condition="almost new"
                ,Description="A fantasy thriller book bought a year ago , the first part of the series , worth reading "},
                        new Books {BookId = 2,Title="ki memeno",Author="Libi klain",Genre="Drama in the family",Condition="good condition"
                ,Description="A suspenseful and emotional story about family, secrets and strengthening faith."},
                                    new Books {BookId = 3,Title="a gumi",Author="Menucha fux",Genre="children's book",Condition="new"
                ,Description="explanation for children about rubber its origin, history and educational values."}
        };
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            if (!books.Any())
                return NotFound("No books found");
            return Ok(books);
        }
        [HttpGet("author/{Author}")]
        public IActionResult GetBooksByAuthor(string authorName)
        {
            var filteredBooks = books.FindAll(b => b.Author.ToLower() == authorName.ToLower());
            if (filteredBooks.Count == 0)
                return NotFound($"No books found by author {authorName}");
            return Ok(filteredBooks);
        }

        [HttpGet("genre/{Genre}")]
        public IActionResult GetBooksByGerds(string gerdsBook)
        {
            var filteredBooks = books.FindAll(B => B.Genre.ToLower() == gerdsBook.ToLower());
            if (filteredBooks.Count == 0)
                return NotFound($"No books found from category {gerdsBook}");
            return Ok(filteredBooks);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Books newBook)
        {
            newBook.BookId = books.Count + 1;
            books.Add(newBook);
            return Ok(newBook);
        }
    }
}
