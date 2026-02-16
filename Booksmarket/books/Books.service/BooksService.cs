using books;
using Books.core.Entities;
using Books.core.Repositories;
using Books.core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Books.service
{
    public class BooksService:IBooksService
    {
        private readonly IBooksRepository _BooksRepository;
        public BooksService(IBooksRepository BooksRepository)
        {
            _BooksRepository = BooksRepository;
        }
  
        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _BooksRepository.GetAllAsync();
            return books.Where(b => b.IsApproved).ToList();
        }
        public async Task<List<Book>> GetPendingBooks()
        {
            var allBooks = await _BooksRepository.GetAllAsync();
            // מחזירים רק ספרים שטרם אושרו
            return allBooks.Where(b => !b.IsApproved).ToList();
        }
        // 2. פונקציית אישור למנהל
        public async Task<Book> ApproveBook(int id)
        {
            var book = await _BooksRepository.GetByIdAsync(id);
            if (book != null)
            {
                book.IsApproved = true;
                await _BooksRepository.save();
            }
            return book;
        }
        public async Task < List<Book>> GetBooksByAuthor(string author)
        {
            return await _BooksRepository.GetByAuthorAsync(author);
        }

        public async Task < List<Book>> GetBooksByGenre(string genre)
        {
            return await _BooksRepository.GetByGenreAsync(genre);
        }
  
        public async Task < Book> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return null;
            }
            var book = await _BooksRepository.AddAsync(newBook);
           await _BooksRepository.save();
            return book;
        }

    }
}
