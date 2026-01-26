using books;
using Books.core.Entities;
using Books.core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
            newUser.status = true;
           await _context.users.AddAsync(newUser);
            return newUser;
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
