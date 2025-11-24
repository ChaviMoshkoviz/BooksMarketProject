using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Service
{
    public interface IBooksService
    {
        List<Book> GetAllBooks();
        List<Book> GetBooksByGenre(string Genre);
        List<Book> GetBooksByAuthor(string Author);
        Book AddBook(Book book);
    }
}
