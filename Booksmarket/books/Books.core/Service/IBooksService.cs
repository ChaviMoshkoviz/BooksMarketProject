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
        List<Book> GetBooksByGenre(string genre);
        List<Book> GetBooksByAuthor(string author);
        Book AddBook(Book book);
    }
}
