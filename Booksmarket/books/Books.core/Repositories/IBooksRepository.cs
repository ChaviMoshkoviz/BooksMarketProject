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
       Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>>GetByGenreAsync(string genre);
        Task<IEnumerable<Book>> GetByAuthorAsync(string author);
        Task AddAsync(Book book);


    }
}
