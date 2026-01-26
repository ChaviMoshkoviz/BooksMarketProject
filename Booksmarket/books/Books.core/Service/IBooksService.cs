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
       Task < List<Book>> GetAllBooks();
      Task <  List<Book>> GetBooksByGenre(string Genre);
      Task <  List<Book>> GetBooksByAuthor(string Author);
       Task < Book> AddBook(Book book);
    }
}
