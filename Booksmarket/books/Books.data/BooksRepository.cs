using books;
using Books.core.Entities;
using Books.core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.data
{
    public class BooksRepository: IBooksRepository
    {
        

        private readonly DataContext _context;
        public BooksRepository(DataContext context)
        {
            _context = context;
        }
        public async Task< List<Book>> GetAllAsync()
        {
            return await _context.books.ToListAsync();
        }
        public async Task< List<Book>> GetByGenreAsync(string genre)
        {
            return await _context.books
                .Where(b => b.Genre.ToLower() == genre.ToLower()).ToListAsync();
        

        }
        public async Task< List<Book>> GetByAuthorAsync(string author)
        {
            return await _context.books // הוספת .Books
                 .Where(b => b.Author.ToLower() == author.ToLower())
                . ToListAsync();
         

        }
        public async Task< Book >AddAsync(Book book)
        {
          
        
           
           await _context.books.AddAsync(book);
        
            return book;
;
        }

        public async Task save()
        {
           await _context.SaveChangesAsync();
        }

    }
}
