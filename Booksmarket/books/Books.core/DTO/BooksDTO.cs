using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.DTO
{
    public class BooksDTO
    {
        public string Title { get; set; }/* שם ספר*/
        public string Author { get; set; } /* שם מחבר*/
        public string Genre { get; set; } /* סוג/ קטגוריה*/
        public string Condition { get; set; } /* מצב (חדש/טוב/ישן.) וכו*/
        public string Description { get; set; } /* תיאור כללי*/
    }
}
