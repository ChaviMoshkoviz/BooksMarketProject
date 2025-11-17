using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Repositories
{
    public interface IBooksRepository
    {
        List<Book> GetAllAsync();
        List<Book>GetByGenreAsync(string genre);
        List<Book> GetByAuthorAsync(string author);
       Book AddAsync(Book book);


    }
}
