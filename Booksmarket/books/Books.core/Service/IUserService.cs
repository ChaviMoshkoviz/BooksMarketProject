using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Service
{
    public interface IUserService
    {
       Task< List<Users>> GetActivUsers();
       Task< Users> GetUserById(int id);
       Task< Users> RegisterUser(Users newUser);
        public Task<Users> GetByEmailAndPassword(string email, string password);
        Task< Users > UpdateUser(int id, Users newUser);
       Task< Users> ChangeUserStatus(int id);
    }
}
