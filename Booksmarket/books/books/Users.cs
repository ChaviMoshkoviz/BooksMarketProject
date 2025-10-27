namespace books
{
    public class Users
    {
        public int UserId { get; set; } /* מזהה משתמש*/
        public string FullName { get; set; } /* שם מלא*/
        public string Email { get; set; } /* מייל*/
        public string Phone { get; set; } /* טלפון*/
        public string City { get; set; } /* עיר*/
        public bool status { get; set; } = true; /*סטטוס פעיל/לא פעיל*/
        
    }
}
