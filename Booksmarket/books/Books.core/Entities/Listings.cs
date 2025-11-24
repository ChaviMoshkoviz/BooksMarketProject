using System.ComponentModel.DataAnnotations;

namespace  Books.core.Entities
{
    public class Listings
    {
        [Key]
        public int ListingId { get; set; } /* מזהה מודעה*/
        public int UserId { get; set; } /* מזהה המשתמש שפרסם*/
        public Users Users { get; set; }/* קשר של יחיד לרבים למחלקת משתמשים*/
        public int BookId { get; set; } /* מזהה ספר שפורסם*/
        public Book Book { get; set; } /* קשר של יחיד לרבים למחלקת ספרים*/
        public string ActionType { get; set; } /* סוג פעולה - מכירה או מסירה*/
        public decimal Price { get; set; } /* מחיר */
        public DateTime DatePosted { get; set; }= DateTime.Now; /*תאריך פרסום*/
        public bool IsActiv {  get; set; } /*אם המודעה פעילה*/
    }
}
