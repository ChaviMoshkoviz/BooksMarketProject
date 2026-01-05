using books;
using Books.core.Entities;
using Books.core.Repositories;
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
        public List<Users> GetActivUsers()
        {
            return _context.users.Where(u => u.status).ToList();
        }
        public Users GetUserById(int id)
        {
            return _context.users.FirstOrDefault(u => u.UserId == id && u.status);
        }
        public Users RegisterUser(Users newUser)
        {
       
            newUser.status = true;
            _context.users.Add(newUser);
            return newUser;
        }

        public Users UpdateUser(int id, Users newUser)
        {
            var user = _context.users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return null;
            user.FullName = newUser.FullName;
            user.Email = newUser.Email;
            user.Phone = newUser.Phone;
            user.City = newUser.City;
            return user;
        }
        public Users DeactivateUser(int id)
        {
            var user = _context.users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return null;
            user.status = false;
            return user;
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
