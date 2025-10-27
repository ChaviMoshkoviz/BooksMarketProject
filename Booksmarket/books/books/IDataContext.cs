namespace books
{
    public interface IDataContext
    {
        public List<Users> users { get; set; }
        public List<Books> books { get; set; }
        public List<Listings> listing { get; set; }

    }
}
