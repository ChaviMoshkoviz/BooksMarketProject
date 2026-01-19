using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.core.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // כי את מזינה את הת"ז ידנית
        [Required(ErrorMessage = "מספר זהות הוא שדה חובה")]
        public int UserId { get; set; } /* מזהה משתמש*/
        [Required(ErrorMessage = "שם מלא הוא שדה חובה")]
        public string FullName { get; set; } /* שם מלא*/
        [Required(ErrorMessage = "מייל הוא שדה חובה")]
        public string Email { get; set; } /* מייל*/
        public string Phone { get; set; } /* טלפון*/
        public string City { get; set; } /* עיר*/
        public bool status { get; set; } = true; /*סטטוס פעיל/לא פעיל*/
        public List<Listings> Listings { get; set; }/*כל המודעות שפורסמו */

    }
}
