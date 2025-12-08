using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.DTO
{
    public class PostListingsDTO
    {
        public int UserId { get; set; } /* מזהה המשתמש שפרסם*/
        public int BookId { get; set; } /* מזהה ספר שפורסם*/
        public string ActionType { get; set; } /* סוג פעולה - מכירה או מסירה*/
        public decimal Price { get; set; } /* מחיר */
        public DateTime DatePosted { get; set; } = DateTime.Now; /*תאריך פרסום*/
    }
}
