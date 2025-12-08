using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.DTO
{
    public class PostUserDTO
    {
        public int UserId { get; set; } /* מזהה משתמש*/
        public string FullName { get; set; } /* שם מלא*/
        public string Email { get; set; } /* מייל*/
        public string Phone { get; set; } /* טלפון*/
        public string City { get; set; } /* עיר*/
    }
}
