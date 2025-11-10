using Books.core.Entities;

namespace books
{
    public interface IDataContext
    {
        public List<Users> users { get; set; }
        public List<Book> books { get; set; }
        public List<Listings> listing { get; set; }

    }
}
