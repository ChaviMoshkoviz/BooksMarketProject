using Books.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace books
{
    public class DataContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Listings> listing { get; set; }
        //public DataContext() 
        //{
        //        users = new List<Users>{new Users { UserId=45,FullName="ronit coen",Email="ronit.coen@gmail.com"
        //            ,Phone="0578965420",City="jerusalem",status=true},
        //        new Users{ UserId=46,FullName="david levi",Email="0578965423l@gmail.com"
        //            ,Phone="0578965423",City="afula",status=false},
        //        new Users{ UserId=47,FullName="shlomi bar",Email="a.bar@bizmail.co.il"
        //         ,Phone="0502487256",City="tel aviv",status=true} };

        //        books = new List<Book>{   /*tension=מתח*/
        //        new Book {BookId = 1,Title="Duplicatim 1",Author="Yonah sapir",Genre="fantasy thriller",Condition="almost new"
        //            ,Description="A fantasy thriller book bought a year ago , the first part of the series , worth reading "},
        //                    new Book {BookId = 2,Title="ki memeno",Author="Libi klain",Genre="Drama in the family",Condition="good condition"
        //            ,Description="A suspenseful and emotional story about family, secrets and strengthening faith."},
        //                                new Book {BookId = 3,Title="a gumi",Author="Menucha fux",Genre="children's book",Condition="new"
        //            ,Description="explanation for children about rubber its origin, history and educational values."} };

        //        listing = new List<Listings> {  new Listings{ListingId=416,UserId=45,BookId=1,ActionType="sale",Price=50,DatePosted=DateTime.Now,IsActiv=true},
        //        new Listings{ListingId=417,UserId=46,BookId=2,ActionType="delivery",Price=0,DatePosted=DateTime.Now,IsActiv=true},
        //        new Listings{ListingId=417,UserId=47,BookId=3,ActionType="sale",Price=2,DatePosted=DateTime.Now,IsActiv=true}};
        //}
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Booksmarket_db");
        }
    }
}
