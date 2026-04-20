using books;
using Books.core.Entities;
using Books.core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Books.data
{
   public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task< List<Users>> GetActivUsers()
        {
            return await _context.users.Where(u => u.status).ToListAsync();
        }
        public async Task< Users >GetUserById(int id)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.UserId == id && u.status);
        }
        public async Task < Users >RegisterUser(Users newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.status = true;
           await _context.users.AddAsync(newUser);
            return newUser;
        }

        public async Task<Users> GetByEmailAndPasswordAsync(string email, string password)
        {
            // 1. קודם כל מוצאים את המשתמש לפי האימייל בלבד
            var user = await _context.users.FirstOrDefaultAsync(u => u.Email == email && u.status);

            // 2. אם המשתמש קיים, בודקים אם הסיסמה שהוזנה תואמת ל-Hash השמור
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            // אם המשתמש לא נמצא או שהסיסמה שגויה
            return null;
        }

        public async Task< Users> UpdateUser(int id, Users newUser)
        {
            var user =await _context.users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return null;
            user.FullName = newUser.FullName;
            user.Email = newUser.Email;
            user.Phone = newUser.Phone;
            user.City = newUser.City;
            return user;
        }
        public async Task< Users> ChangeUserStatus(int id)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return null;
            user.status = !user.status;
            return user;
        }

        public async Task save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
