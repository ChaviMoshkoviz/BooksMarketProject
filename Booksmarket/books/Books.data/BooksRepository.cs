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
        private readonly List<Book> _books = new List<Book>
        {
             new Book {BookId = 1,Title="Duplicatim 1",Author="Yonah sapir",Genre="fantasy thriller",Condition="almost new"
                ,Description="A fantasy thriller book bought a year ago , the first part of the series , worth reading "},
                        new Book {BookId = 2,Title="ki memeno",Author="Libi klain",Genre="Drama in the family",Condition="good condition"
                ,Description="A suspenseful and emotional story about family, secrets and strengthening faith."},
                                    new Book {BookId = 3,Title="a gumi",Author="Menucha fux",Genre="children's book",Condition="new"
                ,Description="explanation for children about rubber its origin, history and educational values."}
        };
       public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await Task.FromResult( _books );
        }
        public async Task<IEnumerable<Book>>GetByGenreAsync(string genre)
        {
            var result=_books.Where(b=>b.Genre.ToLower()==genre.ToLower());
            return await Task.FromResult( result );
        }
        public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            var result = _books.Where(b => b.Author.ToLower() == author.ToLower());
            return await Task.FromResult(result);
        }
        public async Task AddAsync(Book book)
        {
            book.BookId=_books.Max(b=>b.BookId)+1;
            _books.Add(book);
            await Task.CompletedTask;
        }

    }
}
