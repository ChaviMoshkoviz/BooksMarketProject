using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Repositories
{
    public interface IUserRepository
    {
       Task< List<Users>> GetActivUsers();
       Task< Users> GetUserById(int id);
      Task< Users> RegisterUser(Users newUser);
       Task< Users> UpdateUser(int id, Users newUser);
       Task< Users> ChangeUserStatus(int id);
        Task save();
    }
}
