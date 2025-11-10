namespace Books.core.Entities
{
    public class Book
    {
        public int BookId {  get; set; } /* מזהה ספר*/
        public string Title { get; set; }/* שם ספר*/
        public string Author { get; set; } /* שם מחבר*/
        public string Genre { get; set; } /* סוג/ קטגוריה*/
        public string Condition { get; set; } /* מצב (חדש/טוב/ישן.) וכו*/
        public string Description { get; set; } /* תיאור כללי*/

    }
}
