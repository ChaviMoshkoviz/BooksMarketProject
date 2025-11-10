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
    public class BooksService
    {
        private readonly IDataContext _context;
        public BooksService(IDataContext context)
        {
            _context = context;
        }
        public List<Book> GetAllBooks()
        {
            return _context.books;
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            return _context.books.Where(b=> b.Author.ToList() == author.ToList()).ToList();
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            return _context.books.Where(b => b.Genre.ToList() == genre.ToList()).ToList();
        }
        public Book AddBook(Book newBook)
        {
            newBook.BookId = _context.books.Any() ? _context.books.Max(b => b.BookId) + 1 : 1;
            _context.books.Add(newBook);
            return newBook;
        }

    }
}
