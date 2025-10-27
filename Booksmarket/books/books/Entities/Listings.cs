namespace books
{
    public class Listings
    {
        public int ListingId { get; set; } /* מזהה מודעה*/
        public int UserId { get; set; } /* מזהה המשתמש שפרסם*/
        public int BookId { get; set; } /* מזהה ספר שפורסם*/
        public string ActionType { get; set; } /* סוג פעולה - מכירה או מסירה*/
        public decimal Price { get; set; } /* מחיר */
        public DateTime DatePosted { get; set; }= DateTime.Now; /*תאריך פרסום*/
        public bool IsActiv {  get; set; } /*אם המודעה פעילה*/
    }
}
