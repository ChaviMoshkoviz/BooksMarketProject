using books;
using Books.core.Entities;
using Books.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.data
{
    public class BooksRepository: IBooksRepository
    {
        //private readonly List<Book> _books = new List<Book>
        //{
        //     new Book {BookId = 1,Title="Duplicatim 1",Author="Yonah sapir",Genre="fantasy thriller",Condition="almost new"
        //        ,Description="A fantasy thriller book bought a year ago , the first part of the series , worth reading "},
        //                new Book {BookId = 2,Title="ki memeno",Author="Libi klain",Genre="Drama in the family",Condition="good condition"
        //        ,Description="A suspenseful and emotional story about family, secrets and strengthening faith."},
        //                            new Book {BookId = 3,Title="a gumi",Author="Menucha fux",Genre="children's book",Condition="new"
        //        ,Description="explanation for children about rubber its origin, history and educational values."}
        //};

        private readonly DataContext _context;
        public BooksRepository(DataContext context)
        {
            _context = context;
        }
        public List<Book> GetAllAsync()
        {
            return _context.books.ToList();
        }
        public  List<Book> GetByGenreAsync(string genre)
        {

          
            var result = _context.books.Where(b => b.Genre.ToLower() == genre.ToLower()).ToList();
            return result;

        }
        public  List<Book> GetByAuthorAsync(string author)
        {
            var result = _context.books // הוספת .Books
                 .Where(b => b.Author.ToLower() == author.ToLower())
                 .ToList();
            return result;

        }
        public Book AddAsync(Book book)
        {
          
            book.BookId = _context.books.Any() ? _context.books.Max(b => b.BookId) + 1 : 1;

           
            _context.books.Add(book);


            return book;
;
        }

    }
}
