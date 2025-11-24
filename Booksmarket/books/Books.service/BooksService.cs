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
        public List<Book> GetAllBooks()
        {
            return _BooksRepository.GetAllAsync();
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            return _BooksRepository.GetByAuthorAsync(author);
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            return _BooksRepository.GetByGenreAsync(genre);
        }
        public Book AddBook(Book newBook)
        {
            _BooksRepository.save();
            return _BooksRepository.AddAsync(newBook);
        }

    }
}
