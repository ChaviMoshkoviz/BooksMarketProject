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
       Task< List<Book>> GetAllAsync();
       Task< List<Book>>GetByGenreAsync(string genre);
       Task< List<Book> >GetByAuthorAsync(string author);
       Task < Book >AddAsync(Book book);
        Task save();


    }
}
