using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.DTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "חובה להזין מספר זהות")]
        [Range(100000000, 999999999, ErrorMessage = "מספר זהות לא תקין")] // אופציונלי: בדיקת טווח ספרות
        public int UserId { get; set; }
        [Required(ErrorMessage = "שם מלא הוא שדה חובה")]
        public string FullName { get; set; } /* שם מלא*/
        [Required(ErrorMessage = "מייל הוא שדה חובה")]
        public string Email { get; set; } /* מייל*/
        public string Phone { get; set; } /* טלפון*/
        public string City { get; set; } /* עיר*/
    
    }
}
